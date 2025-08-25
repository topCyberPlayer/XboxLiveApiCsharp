using Domain.DTO;
using Domain.Entities.JsonModels;
using Domain.Entities.XblAuth;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.XboxLiveService;

namespace Application.XboxLiveUseCases
{
    public class AuthenticationUseCase(
        IXboxLiveAuthenticationService authService,
        IAuthenticationRepository authRepository)
    {
        internal readonly IXboxLiveAuthenticationService _authService = authService;
        internal readonly IAuthenticationRepository _authRepository = authRepository;

        private OAuthTokenJson? _authToken;

        public async Task<IEnumerable<DonorDTO>?> GetAllDonors() =>
            await _authRepository.GetAllDonorsAsync(o => new DonorDTO
            {
                UserId = o.UserId,
                XboxLiveNotAfter = o.XboxXauTokenLink!.NotAfter,
                XboxUserNotAfter = o.XboxXauTokenLink!.XboxXstsTokenLink!.NotAfter,
                Xuid = o.XboxXauTokenLink!.XboxXstsTokenLink!.Xuid,
                Gamertag = o.XboxXauTokenLink!.XboxXstsTokenLink!.Gamertag
            });

        public string GenerateAuthorizationUrl() => 
            _authService.GenerateAuthorizationUrl();

        /// <summary>
        /// Использовать когда TokenOAuth вообще нет
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokensAsync(string authorizationCode)
        {
            _authToken = await _authService.RequestOauth2Token(authorizationCode)
                ?? throw new InvalidOperationException("Failed to retrieve OAuth token.");

            await ExchangeTokensAsync();
        }

        /// <summary>
        /// Использовать когда TokenOAuth есть, но Дата окончания истекла
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async ValueTask<string> GetValidAuthHeaderAsync()
        {
            if (!IsUserTokenExpired())
                return _authRepository.GetAuthorizationHeaderValue();

            XboxAuthToken expiredTokenOAuth = await _authRepository.GetXboxAuthToken();
            
            if (expiredTokenOAuth is null)
            {
                throw new InvalidOperationException(
                    $"No stored OAuth token found. User interaction required. Go to: {GenerateAuthorizationUrl()}");
            }

            _authToken = await _authService.RefreshOauth2Token(expiredTokenOAuth)
                ?? throw new InvalidOperationException("Failed to retrieve OAuth token.");

            await ExchangeTokensAsync();

            return _authRepository.GetAuthorizationHeaderValue();
        }

        /// <summary>
        /// Обменивает OAuth-токен на XAU и XSTS и сохраняет в репозиторий.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task ExchangeTokensAsync()
        {
            XauTokenJson xauToken = await _authService.RequestXauToken(_authToken)
                ?? throw new InvalidOperationException("Failed to retrieve XAU token.");

            XstsTokenJson xstsToken = await _authService.RequestXstsToken(xauToken)
                ?? throw new InvalidOperationException("Failed to retrieve XSTS token.");

            await _authRepository.SaveOrUpdateTokensAsync(_authToken, xauToken, xstsToken);
        }

        private bool IsUserTokenExpired() => 
            DateTime.UtcNow > _authRepository.GetDateUserTokenExpired();

        private bool IsLiveTokenExpired() => 
            DateTime.Now > _authRepository.GetDateLiveTokenExpired();
    }
}
