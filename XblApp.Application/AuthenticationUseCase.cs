using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application
{
    public class AuthenticationUseCase
    {
        internal readonly IXboxLiveAuthenticationService _authService;
        internal readonly IAuthenticationRepository _authRepository;

        private TokenOAuth? _tokenOAuth;
        private TokenXau? _tokenXau;
        private TokenXsts? _tokenXsts;

        public AuthenticationUseCase(
            IXboxLiveAuthenticationService authService, 
            IAuthenticationRepository authRepository)
        {
            _authService = authService;
            _authRepository = authRepository;
        }

        public string GenerateAuthorizationUrl() => _authService.GenerateAuthorizationUrl();

        /// <summary>
        /// Использовать когда TokenOAuth вообще нет
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokens(string authorizationCode)
        {
            _tokenOAuth = await _authService.RequestOauth2Token(authorizationCode)
                ?? throw new InvalidOperationException("Failed to retrieve OAuth token.");

            await BaseTokens();
        }

        /// <summary>
        /// Использовать когда TokenOAuth есть, но Дата окончания истекла
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CheckDateOfExpiry()
        {
            var resultTokenXau = IsDateXauTokenExperid();

            if (!resultTokenXau)
                return;

            TokenOAuth expiredTokenOAuth = await _authRepository.GetTokenOAuth();
            
            _tokenOAuth = await _authService.RefreshOauth2Token(expiredTokenOAuth)
                ?? throw new InvalidOperationException("Failed to retrieve OAuth token.");

            await BaseTokens();
        }

        private async Task BaseTokens()
        {
            _tokenXau = await _authService.RequestXauToken(_tokenOAuth)
                ?? throw new InvalidOperationException("Failed to retrieve XAU token.");

            _tokenXsts = await _authService.RequestXstsToken(_tokenXau)
                ?? throw new InvalidOperationException("Failed to retrieve XSTS token.");

            await SaveTokens();
        }

        private async Task SaveTokens()
        {
            await _authRepository.SaveTokenAsync(_tokenOAuth);
            await _authRepository.SaveTokenAsync(_tokenXau);
            await _authRepository.SaveTokenAsync(_tokenXsts);
        }

        private bool IsDateXstsTokenExperid()
        {
            DateTime? dateNow = DateTime.Now;

            DateTime? dateDb = _authRepository.GetDateXstsTokenExpired();

            return dateNow > dateDb ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true - истек</returns>
        private bool IsDateXauTokenExperid()
        {
            DateTime? dateNow = DateTime.Now;

            DateTime? dateDb = _authRepository.GetDateXauTokenExpired();

            return dateNow > dateDb ? true : false;
        }
    }
}
