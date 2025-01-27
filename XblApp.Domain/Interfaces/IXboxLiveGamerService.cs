using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveGamerService
    {
        public Task<List<Gamer>> GetGamerProfileAsync(string gamertag);
        public Task<List<Gamer>> GetGamerProfileAsync(long xuid);
    }
}
