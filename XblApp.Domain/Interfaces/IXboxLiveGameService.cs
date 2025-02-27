using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveGameService 
    {
        public Task<List<Game>> GetGamesForGamerProfileAsync(string gamertag);
        public Task<List<Game>> GetGamesForGamerProfileAsync(long xuid);
    }
}
