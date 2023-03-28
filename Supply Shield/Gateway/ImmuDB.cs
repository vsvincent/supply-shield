using CodeNotary.ImmuDb;

namespace Gateway
{
    public class ImmuDB
    {
        ImmuClient client;
        public ImmuDB() {
            client = new ImmuClient("localhost", 9494);
            CreateDatabase();
        }
        public async void CreateDatabase()
        {
            await client.LoginAsync("immudb", "immudb");
        }
        public async Task<string> Read()
        {
            await client.LoginAsync("immudb", "immudb");
            await client.UseDatabaseAsync("supplyshielddb");
            await client.SetRawAsync("k123", new byte[] { 1, 2, 3 });
            return await client.GetAsync("k123");
        }
    }
}
