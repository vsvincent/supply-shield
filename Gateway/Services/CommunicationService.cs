using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Gateway.Services
{
    public class CommunicationService : BackgroundService
    {
        private bool _isInitialized = false;
        private ConnectionFactory _factory = new ConnectionFactory();
        private IConnection _connection;
        private IModel _channel;
        private readonly string _requestQueue = "incident-in";
        private readonly string _responseQueue = "incident-out";
        public CommunicationService()
        {

            if (!_isInitialized)
            {
                _factory.Uri = new Uri("amqp://admin:password@34.118.82.210:5672/");
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(
                    queue: _responseQueue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                //unlike channels, connections are expensive and should be reused
                AppDomain.CurrentDomain.ProcessExit += DisposeConnection;

                _isInitialized = true;
            }
        }
        private void DisposeConnection(object sender, EventArgs e)
        {
            _connection.Close();
            _connection.Dispose();
        }
        /// <summary>
        /// Sends a request for and awaits a response with an item
        /// </summary>
        /// <param name="requestObject"></param>
        /// <returns></returns>
        /// <exception cref="TimeoutException"></exception>
        public async Task<T> GetItem<T>(object item)
        {
            // Create a unique correlation ID for this request
            var correlationId = Guid.NewGuid().ToString();

            // Declare a queue for the response
            string responseQueueName =
                _channel.QueueDeclare(
                    queue: _responseQueue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                ).QueueName;

            // Set up a consumer to receive the response
            var consumer = new AsyncEventingBasicConsumer(_channel);
            var responseReceived = new TaskCompletionSource<T>();
            consumer.Received += async (model, ea) =>
            {
                // Check if this is the correlating response
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    T responseObject = JsonConvert.DeserializeObject<T>(response);
                    responseReceived.SetResult(responseObject);
                }
            };
            _channel.BasicConsume(responseQueueName, true, consumer);

            // Send the request with the correlation ID set
            var props = _channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = responseQueueName;
            var requestBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _requestQueue,
                basicProperties: props,
                body: requestBytes
                );

            // Wait for the response
            TimeoutTask();

            return await responseReceived.Task;
        }
        /// <summary>
        /// Sends a request for and awaits a response with an item
        /// </summary>
        /// <param name="requestObject"></param>
        /// <returns></returns>
        /// <exception cref="TimeoutException"></exception>
        public async void CreateItem<T>(object item, string requestQueue)
        {
            // Create a unique correlation ID for this request
            var correlationId = Guid.NewGuid().ToString();

            // Send the request with the correlation ID set
            var props = _channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            var requestBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: requestQueue,
                basicProperties: props,
                body: requestBytes
                );
        }
        private async void TimeoutTask()
        {
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(60));
            if (await Task.WhenAny(timeoutTask) == timeoutTask)
            {
                throw new TimeoutException("Timed out waiting for response");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
