using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveAuthenticationService
    {
        public Task<XboxOAuthToken> RequestOauth2Token(string authorizationCode);
        public Task<XboxLiveToken> RequestXauToken(XboxOAuthToken authToken);
        public Task<XboxUserToken> RequestXstsToken(XboxLiveToken liveToken);

        /// <summary>
        /// Возвращает новый (обновляет) Oauth2Token
        /// </summary>
        /// <param name="expiredAuthToken"></param>
        /// <returns></returns>
        public Task<XboxOAuthToken> RefreshOauth2Token(XboxOAuthToken expiredAuthToken);
        public string GenerateAuthorizationUrl();
        public Task<string> GetValidTokenAsync();
    }
}
