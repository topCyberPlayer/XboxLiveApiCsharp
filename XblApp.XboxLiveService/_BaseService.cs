using System.Net.Http.Json;

namespace XblApp.XboxLiveService
{
    public abstract class BaseService
    {
        internal readonly IHttpClientFactory factory;

        protected BaseService(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public async Task<T> SendRequestAsync<T>(HttpClient client, string uri)
        {
            using HttpResponseMessage response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadFromJsonAsync<T>() 
                ?? throw new InvalidOperationException($"Failed to deserialize response content to type {typeof(T).Name}.");
        }
    }
}
