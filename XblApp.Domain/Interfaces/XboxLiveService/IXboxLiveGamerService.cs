using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IXboxLiveService
{
    public interface IXboxLiveGamerService
    {
        public Task<GamerJson> GetGamerProfileAsync(string gamertag);
        public Task<GamerJson> GetGamerProfileAsync(long xuid);
    }
}
