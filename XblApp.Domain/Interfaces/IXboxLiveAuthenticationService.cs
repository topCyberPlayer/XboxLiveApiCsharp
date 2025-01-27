using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveAuthenticationService
    {
        public Task<TokenOAuth> RequestOauth2Token(string authorizationCode);
        public Task<TokenXau> RequestXauToken(TokenOAuth tokenOAuth);
        public Task<TokenXsts> RequestXstsToken(TokenXau tokenXau);

        /// <summary>
        /// Возвращает новый (обновляет) Oauth2Token
        /// </summary>
        /// <param name="expiredTokenOAuth"></param>
        /// <returns></returns>
        public Task<TokenOAuth> RefreshOauth2Token(TokenOAuth expiredTokenOAuth);
        public string GenerateAuthorizationUrl();
        
    }
}
