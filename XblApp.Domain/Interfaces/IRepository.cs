using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IGamerRepository
    {
        public Task<Gamer> GetGamerProfile(long id);
        public Task<Gamer> GetGamerProfile(string gamertag);
        public Task<List<Gamer>> GetAllGamerProfilesAsync();
        public Task<Gamer> GetGamesForGamerAsync(string gamertag);
        public void SaveGamer(Gamer gamer);
    }

    public interface IAuthenticationRepository
    {
        public void Save(TokenOAuth tokenXbl);
        public void Save(TokenXau tokenXbl);
        public void Save(TokenXsts tokenXbl);
        public string GetRefreshToken();
        public DateTime GetDateExpired();
        public string GetAuthorizationHeaderValue();
    }
}
