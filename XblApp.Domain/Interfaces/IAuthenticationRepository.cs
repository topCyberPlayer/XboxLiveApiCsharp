using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task SaveTokenAsync(XboxOAuthToken authToken);
        public Task SaveTokenAsync(XboxLiveToken liveToken);
        public Task SaveTokenAsync(XboxUserToken userToken);
        public Task SaveTokensAsync(XboxOAuthToken authToken, XboxLiveToken liveToken, XboxUserToken userToken);
        public Task<XboxOAuthToken> GetTokenAuth();
        public DateTime GetDateUserTokenExpired();
        public DateTime GetDateLiveTokenExpired();
        public string GetAuthorizationHeaderValue();
    }
}
