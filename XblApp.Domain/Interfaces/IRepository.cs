using XblApp.Domain.Entities;
using XblApp.Shared.DTOs;

namespace XblApp.Domain.Interfaces
{
    public interface IGamerRepository
    {
        public Task<List<Gamer>> GetAllGamerProfilesAsync();
        Task<List<Game>> GetGamesForGamerAsync(string gamertag);
        public Gamer GetGamerProfile(long id);
        public Gamer GetGamerProfile(string gamertag);
        public void SaveGamer(GamerDTO gamer);
    }

    public interface IAuthenticationRepository
    {
        public void Save(TokenOAuthDTO tokenXbl);
        public void Save(TokenXauDTO tokenXbl);
        public void Save(TokenXstsDTO tokenXbl);
        public string GetRefreshToken();
        public DateTime GetDateExpired();
        public string GetAuthorizationHeaderValue();
    }
}
