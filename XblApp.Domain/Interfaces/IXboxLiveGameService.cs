using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveGameService 
    {
        public Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag, int maxItems = 5);
        public Task<List<Game>> GetGamesForGamerProfileAsync(long xuid, int maxItems = 5);
    }
}
