using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IGameRepository
    {
        public Task<Game> GetGameAsync(string gameName);
        public Task<List<Game>> GetAllGamesAsync();
    }

    public interface IGamerRepository
    {
        public Task<Gamer> GetGamerProfileAsync(long id);
        public Task<Gamer> GetGamerProfileAsync(string gamertag);
        public Task<List<Gamer>> GetAllGamerProfilesAsync();
        public Task<Gamer> GetGamesForGamerAsync(string gamertag);
        public Task SaveGamerAsync(Gamer gamer);
    }

    public interface IAuthenticationRepository
    {
        public Task SaveAsync(TokenOAuth tokenXbl);
        public Task SaveAsync(TokenXau tokenXbl);
        public Task SaveAsync(TokenXsts tokenXbl);
        public string GetRefreshToken();
        public DateTime GetDateExpired();
        public string GetAuthorizationHeaderValue();
    }
}
