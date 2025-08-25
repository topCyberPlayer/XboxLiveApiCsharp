using Domain.DTO;
using Domain.Entities.JsonModels;
using Domain.Entities.XblAuth;
using System.Linq.Expressions;

namespace Domain.Interfaces.IRepository
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
        public Task<IEnumerable<TKey>?> GetAllDonorsAsync<TKey>(Expression<Func<XboxAuthToken, TKey>> expressionSelect);
    }
}
