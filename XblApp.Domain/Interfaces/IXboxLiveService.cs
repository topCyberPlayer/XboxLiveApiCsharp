using XblApp.Shared.DTOs;

namespace XblApp.Domain.Interfaces
{
    public interface IBaseService
    {
        public Task<T> DeserializeJson<T>(HttpResponseMessage httpResponse);
    }

    public interface IXboxLiveGamerService : IBaseService
    {
        public Task<GamerDTO> GetGamerProfileAsync(string gamertag, string authorizationHeaderValue);
        public Task<GamerDTO> GetGamerProfileAsync(long xuid, string authorizationHeaderValue);
    }

    public interface IXboxLiveGameService : IBaseService 
    {
        //public Task<GameForGamerDTO> GetTitleHistoryAsync(string gamertag, string authorizationHeaderValue, int maxItems = 5);
        //public Task<GameForGamerDTO> GetTitleHistoryAsync(long xuid, string authorizationHeaderValue, int maxItems = 5);
    }

    public interface IAuthenticationService : IBaseService
    {
        public Task<TokenOAuthDTO> RequestOauth2Token(string authorizationCode);
        public Task<TokenXauDTO> RequestXauToken(TokenOAuthDTO tokenOAuth);
        public Task<TokenXstsDTO> RequestXstsToken(TokenXauDTO tokenXau);

        /// <summary>
        /// Возвращает новый (обновляет) Oauth2Token
        /// </summary>
        /// <param name="expiredTokenOAuthDTO"></param>
        /// <returns></returns>
        public Task<TokenOAuthDTO> RefreshOauth2Token(TokenOAuthDTO expiredTokenOAuthDTO);
        public string GenerateAuthorizationUrl();
        
    }
}
