
using IncidentService.Models;
using IncidentService.Repository;
using IncidentService.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.Common;
using System.Text;

namespace IncidentService.Service
{
    public static class CommunicationService
    {
        static readonly IGoogleSecretManager _googleSecretManager = new GoogleSecretManager();
        static readonly IQldbContext _qldbContext = new QldbContext(_googleSecretManager);
        static readonly IIncidentRepository _incidentRepository = new IncidentRepository(_qldbContext);
        static readonly IIncidentService _incidentService = new IncidentService(_incidentRepository);
        static ConnectionFactory _factory = new ConnectionFactory();
        static IConnection _connection;
        static IModel _channel;
        static readonly string _requestQueue = "incident-in";
        static readonly string _responseQueue = "incident-out";
        public static void Initialize()
        {
            AppDomain.CurrentDomain.ProcessExit += DisposeConnection;
            _factory.Uri = new Uri("amqp://admin:password@34.118.82.210:5672/");
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public static async void Listen(string queue)
        {
            _channel.QueueDeclare(queue);

            var queueName = _channel.QueueDeclare(
                    queue: queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                ).QueueName;

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                JObject request = JObject.Parse(message);
                HandleRequest(request, ea.BasicProperties.CorrelationId);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            Console.WriteLine("Users Consuming");
        }

        private static void HandleRequest(JObject request, string correlationId)
        {
            switch (request["type"].ToString())
            {
                case "create":
                    IIncident incident = JsonConvert.DeserializeObject<Incident>(request["data"].ToString());
                    _incidentService.Add(incident);
                    break;
                case "get":
                    _incidentService.GetAll();
                    break;
                default:
                    break;
            }
        }

        public static async void Respond(object item, string correlationId, string exchange = "")
        {

            // Send the request with the correlation ID set
            var props = _channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            var requestBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: _responseQueue,
                basicProperties: props,
                body: requestBytes
                );
        }
        private static void DisposeConnection(object sender, EventArgs e)
        {
            _connection.Close();
            _connection.Dispose();
        }

    }
}
