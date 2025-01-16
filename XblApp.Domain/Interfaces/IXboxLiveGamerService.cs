using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveGamerService
    {
        public Task<Gamer> GetGamerProfileAsync(string gamertag, string authorizationHeaderValue);
        public Task<Gamer> GetGamerProfileAsync(long xuid, string authorizationHeaderValue);
    }
}
