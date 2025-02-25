using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task SaveTokenAsync(XboxOAuthToken tokenXbl);
        public Task SaveTokenAsync(XboxLiveToken tokenXbl);
        public Task SaveTokenAsync(XboxUserToken tokenXbl);
        public Task<XboxOAuthToken> GetTokenOAuth();
        public DateTime GetDateXstsTokenExpired();
        public DateTime GetDateXauTokenExpired();
        public string GetAuthorizationHeaderValue();
    }
}
