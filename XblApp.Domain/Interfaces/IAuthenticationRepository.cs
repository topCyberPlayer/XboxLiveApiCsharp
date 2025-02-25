using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task SaveTokensAsync(XboxOAuthToken authToken, XboxLiveToken liveToken, XboxUserToken userToken);
        public Task<XboxOAuthToken> GetXboxAuthToken();
        public DateTime GetDateUserTokenExpired();
        public DateTime GetDateLiveTokenExpired();
        public string? GetAuthorizationHeaderValue();
        public Task<List<(string UserId, DateTime XboxLiveNotAfter, DateTime XboxUserNotAfter, string Xuid, string Gamertag)>?> GetAllDonorsAsync();
    }
}
