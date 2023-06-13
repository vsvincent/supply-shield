namespace Gateway.Services
{
    public interface IClientService
    {
        Task<string> Get(string url);
        Task<string> Post(string url, string jsonContent);
    }
}
