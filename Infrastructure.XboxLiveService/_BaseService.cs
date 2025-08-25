using System.Net.Http.Json;

namespace Infrastructure.XboxLiveService
{
    public abstract class BaseService
    {
        internal readonly IHttpClientFactory factory;

        protected BaseService(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        protected async Task<T> SendRequestAsync<T>(HttpClient client, string uri)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                T? result = await response.Content.ReadFromJsonAsync<T>()
                    ?? throw new InvalidOperationException($"Failed to deserialize response content to type {typeof(T).Name}.");

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
