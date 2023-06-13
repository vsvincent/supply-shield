using System.Text;

namespace Gateway.Services
{
    public class ClientService
    {
        private readonly HttpClient httpClient;

        public ClientService()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> Get(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task<string> Post(string url, string jsonContent)
        {
            HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
