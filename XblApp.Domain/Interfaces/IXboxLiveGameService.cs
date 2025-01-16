using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveGameService 
    {
        public Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag, string authorizationHeaderValue, int maxItems = 5);
        public Task<List<Game>> GetGamesForGamerProfileAsync(long xuid, string authorizationHeaderValue, int maxItems = 5);
    }
}
