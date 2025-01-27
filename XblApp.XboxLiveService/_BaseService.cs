using System.Net.Http.Json;
using XblApp.Domain.Interfaces;

namespace XblApp.XboxLiveService
{
    public abstract class BaseService
    {
        internal readonly IHttpClientFactory factory;
        internal readonly string authorizationHeaderValue;

        protected BaseService(IHttpClientFactory factory, IAuthenticationRepository authRepository)
        {
            this.factory = factory;
            this.authorizationHeaderValue = authRepository.GetAuthorizationHeaderValue()
                ?? throw new InvalidOperationException("Authorization header value cannot be null.");
        }

        public async Task<T> SendRequestAsync<T>(HttpClient client, string uri)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(uri);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return await response.Content.ReadFromJsonAsync<T>()
                    ?? throw new InvalidOperationException($"Failed to deserialize response content to type {typeof(T).Name}.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
