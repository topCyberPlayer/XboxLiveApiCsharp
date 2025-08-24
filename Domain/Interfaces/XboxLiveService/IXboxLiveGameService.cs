using XblApp.Domain.Entities.JsonModels;

namespace XblApp.Domain.Interfaces.IXboxLiveService
{
    public interface IXboxLiveGameService
    {
        public Task<GameJson> GetGamesForGamerProfileAsync(string gamertag);
        public Task<GameJson> GetGamesForGamerProfileAsync(long xuid);
    }
}
