using Domain.Entities.JsonModels;

namespace Domain.Interfaces.XboxLiveService
{
    public interface IXboxLiveGamerService
    {
        public Task<GamerJson> GetGamerProfileAsync(string gamertag);
        public Task<GamerJson> GetGamerProfileAsync(long xuid);
    }
}
