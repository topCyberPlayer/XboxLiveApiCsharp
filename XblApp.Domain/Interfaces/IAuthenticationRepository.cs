using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task SaveOrUpdateTokensAsync(XboxOAuthToken authToken, XboxLiveToken liveToken, XboxUserToken userToken);
        public Task<XboxOAuthToken> GetXboxAuthToken();
        /// <summary>
        /// Живет 16 часов
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateUserTokenExpired();
        /// <summary>
        /// Живет 4 дня
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateLiveTokenExpired();
        public string? GetAuthorizationHeaderValue();
        public Task<List<(string UserId, DateTime XboxLiveNotAfter, DateTime XboxUserNotAfter, string Xuid, string Gamertag)>?> GetAllDonorsAsync();
    }
}
