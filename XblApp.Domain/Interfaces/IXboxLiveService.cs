using XblApp.Shared.DTOs;

namespace XblApp.Domain.Interfaces
{
    public interface IBaseService
    {
        public Task<T> DeserializeJson<T>(HttpResponseMessage httpResponse);
    }

    public interface IXboxLiveGamerService : IBaseService
    {
        public Task<GamerDTO> GetGamerProfileAsync(string gamertag, string authorizationCode);
        public Task<GamerDTO> GetGamerProfileAsync(long xuid, string authorizationCode);
    }

    public interface IXboxLiveGameService : IBaseService 
    {

    }

    public interface IAuthenticationService : IBaseService
    {
        public Task<TokenOAuthDTO> RequestOauth2Token(string authorizationCode);
        public Task<TokenXauDTO> RequestXauToken(TokenOAuthDTO tokenOAuth);//TokenOAuthModelXbl tokenOAuth);
        public Task<TokenXstsDTO> RequestXstsToken(TokenXauDTO tokenXau);//TokenXauModelXbl tokenXau);
        public Task<TokenOAuthDTO> RefreshOauth2Token(string refreshToken);
        public string GenerateAuthorizationUrl();
        
    }
}
