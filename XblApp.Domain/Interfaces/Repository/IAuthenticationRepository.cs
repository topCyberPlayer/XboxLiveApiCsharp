using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IAuthenticationRepository
    {
        public Task SaveOrUpdateTokensAsync(OAuthTokenJson authToken, XauTokenJson liveToken, XstsTokenJson userToken);
        public Task<XboxAuthToken> GetXboxAuthToken();
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
