using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveAuthenticationService
    {
        public Task<XboxOAuthToken> RequestOauth2Token(string authorizationCode);
        public Task<XboxLiveToken> RequestXauToken(XboxOAuthToken tokenOAuth);
        public Task<XboxUserToken> RequestXstsToken(XboxLiveToken tokenXau);

        /// <summary>
        /// Возвращает новый (обновляет) Oauth2Token
        /// </summary>
        /// <param name="expiredTokenOAuth"></param>
        /// <returns></returns>
        public Task<XboxOAuthToken> RefreshOauth2Token(XboxOAuthToken expiredTokenOAuth);
        public string GenerateAuthorizationUrl();
    }
}
