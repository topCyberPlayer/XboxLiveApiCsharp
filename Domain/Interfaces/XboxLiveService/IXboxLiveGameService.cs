using Domain.Entities.JsonModels;

namespace Domain.Interfaces.XboxLiveService
{
    public interface IXboxLiveGameService
    {
        public Task<GameJson> GetGamesForGamerProfileAsync(string gamertag);
        public Task<GameJson> GetGamesForGamerProfileAsync(long xuid);
    }
}
