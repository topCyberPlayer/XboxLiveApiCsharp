using XblApp.Shared.DTOs;

namespace XblApp.Domain.Interfaces
{
    public interface IGameRepository
    {
        public Task<GameDTO> GetGameAsync(string gameName);
        public Task<List<GameDTO>> GetAllGamesAsync();
    }

    public interface IGamerRepository
    {
        public Task<GamerDTO> GetGamerProfileAsync(long id);
        public Task<GamerDTO> GetGamerProfileAsync(string gamertag);
        public Task<List<GamerDTO>> GetAllGamerProfilesAsync();
        public Task<GamerGameDTO> GetGamesForGamerAsync(string gamertag);
        public Task SaveGamerAsync(GamerDTO gamer);
    }

    public interface IAuthenticationRepository
    {
        public Task SaveAsync(TokenOAuthDTO tokenXbl);
        public Task SaveAsync(TokenXauDTO tokenXbl);
        public Task SaveAsync(TokenXstsDTO tokenXbl);
        public Task<TokenOAuthDTO> GetTokenOAuth();
        public DateTime GetDateXstsTokenExpired();
        public DateTime GetDateXauTokenExpired();
        public string GetAuthorizationHeaderValue();
    }
}
