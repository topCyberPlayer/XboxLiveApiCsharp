using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IXboxLiveService
{
    public interface IXboxLiveAuthenticationService
    {
        public Task<OAuthTokenJson> RequestOauth2Token(string authorizationCode);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken">Значение мб получено как из XboxLive, так и из БД</param>
        /// <returns></returns>
        public Task<XauTokenJson> RequestXauToken(OAuthTokenJson token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">Значение мб получено как из XboxLive, так и из БД</param>
        /// <returns></returns>
        public Task<XstsTokenJson> RequestXstsToken(XauTokenJson token);
        /// <summary>
        /// Возвращает новый (обновляет) Oauth2Token
        /// </summary>
        /// <param name="expiredAuthToken">Значение мб получено только из БД</param>
        /// <returns></returns>
        public Task<OAuthTokenJson> RefreshOauth2Token(XboxAuthToken expiredAuthToken);
        
        public string GenerateAuthorizationUrl();
    }
}
