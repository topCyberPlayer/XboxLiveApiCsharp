using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.Application.XboxLiveUseCases
{
    public class AuthenticationUseCase
    {
        internal readonly IXboxLiveAuthenticationService _authService;
        internal readonly IAuthenticationRepository _authRepository;

        private OAuthTokenJson? _authToken;

        public AuthenticationUseCase(
            IXboxLiveAuthenticationService authService, 
            IAuthenticationRepository authRepository)
        {
            _authService = authService;
            _authRepository = authRepository;
        }

        public async Task<List<(string UserId, DateTime XboxLiveNotAfter, DateTime XboxUserNotAfter, string Xuid, string Gamertag)>?> GetAllDonors() => 
            await _authRepository.GetAllDonorsAsync();

        public string GenerateAuthorizationUrl() => _authService.GenerateAuthorizationUrl();

        /// <summary>
        /// Использовать когда TokenOAuth вообще нет
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokens(string authorizationCode)
        {
            _authToken = await _authService.RequestOauth2Token(authorizationCode)
                ?? throw new InvalidOperationException("Failed to retrieve OAuth token.");

            await BaseTokens();
        }

        /// <summary>
        /// Использовать когда TokenOAuth есть, но Дата окончания истекла
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async ValueTask<string> GetValidAuthHeaderAsync()
        {
            bool isExpired = IsDateUserTokenExperid();

            if (!isExpired)
                return _authRepository.GetAuthorizationHeaderValue();

            XboxAuthToken expiredTokenOAuth = await _authRepository.GetXboxAuthToken();

            if (expiredTokenOAuth is null)
            {
                string authorizationUrl = GenerateAuthorizationUrl();

                //todo Этот веб адрес(authorizationUrl) надо переадрисовать или выбросить исключение
            }

            _authToken = await _authService.RefreshOauth2Token(expiredTokenOAuth)
                ?? throw new InvalidOperationException("Failed to retrieve OAuth token.");

            await BaseTokens();

            return _authRepository.GetAuthorizationHeaderValue();
        }

        private async Task BaseTokens()
        {
            XauTokenJson xauToken = await _authService.RequestXauToken(_authToken)
                ?? throw new InvalidOperationException("Failed to retrieve XAU token.");

            XstsTokenJson xstsToken = await _authService.RequestXstsToken(xauToken)
                ?? throw new InvalidOperationException("Failed to retrieve XSTS token.");

            await _authRepository.SaveOrUpdateTokensAsync(_authToken,xauToken, xstsToken);
        }

        private bool IsDateUserTokenExperid() => DateTime.UtcNow > _authRepository.GetDateUserTokenExpired();

        private bool IsDateLiveTokenExperid() => DateTime.Now > _authRepository.GetDateLiveTokenExpired();
    }
}
