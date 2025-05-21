using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace XblApp.XboxLiveService.XboxLiveServices.AchievementServices
{
    public abstract class BaseAchievementService<TAchivoJson, KAchivosInnerJson>
    where TAchivoJson : class, new()
    where KAchivosInnerJson : class
    {
        protected readonly IHttpClientFactory factory;
        protected abstract string HttpClientName { get; }
        protected abstract List<KAchivosInnerJson> GetInnerList(TAchivoJson json);
        protected abstract void SetInnerList(TAchivoJson json, List<KAchivosInnerJson> list);
        protected abstract void SetGamerId(TAchivoJson json, long xuid);
        protected abstract string ContractVersion { get; }

        protected BaseAchievementService(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public async Task<TAchivoJson> GetAllAchievementsForGamerAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";
            return await GetAchievementBaseAsync(relativeUrl, xuid);
        }

        public async Task<TAchivoJson> GetAchievementsForOneGameAsync(long xuid, long titleId)
        {
            string relativeUrl = $"/users/xuid({xuid})/achievements";

            Dictionary<string, string> queryParams = new()
            {
                { "titleId", titleId.ToString() },
            };

            string uri = QueryHelpers.AddQueryString(relativeUrl, queryParams);
            return await GetAchievementBaseAsync(uri, xuid);
        }

        private async Task<TAchivoJson> GetAchievementBaseAsync(string relativeUrl, long xuid)
        {
            HttpClient client = factory.CreateClient(HttpClientName);
            client.DefaultRequestHeaders.Add("x-xbl-contract-version", ContractVersion);

            TAchivoJson result = await SendPaginatedRequestAsync(client, relativeUrl);
            SetGamerId(result, xuid);
            return result;
        }

        private async Task<TAchivoJson> SendPaginatedRequestAsync(HttpClient client, string baseUri)
        {
            string? continuationToken = null;
            List<KAchivosInnerJson> accumulated = new();
            TAchivoJson result = new();

            do
            {
                string requestUri = string.IsNullOrEmpty(continuationToken) ? 
                    baseUri : QueryHelpers.AddQueryString(baseUri, "continuationToken", continuationToken);

                using HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                var currentPage = await response.Content.ReadFromJsonAsync<TAchivoJson>()
                    ?? throw new InvalidOperationException($"Cannot deserialize response to {typeof(TAchivoJson).Name}");

                List<KAchivosInnerJson> pageItems = GetInnerList(currentPage);
                if (pageItems != null)
                    accumulated.AddRange(pageItems);

                continuationToken = (currentPage as dynamic).PagingInfo?.ContinuationToken;

            } while (!string.IsNullOrEmpty(continuationToken));

            SetInnerList(result, accumulated);
            return result;
        }
    }

}
