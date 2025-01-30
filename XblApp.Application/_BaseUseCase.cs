using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application
{
    public class BaseUseCase
    {
        internal readonly IXboxLiveAuthenticationService _authService;
        internal readonly IAuthenticationRepository _authRepository;

        public BaseUseCase(IXboxLiveAuthenticationService authService, IAuthenticationRepository authRepository)
        {
            _authService = authService;
            _authRepository = authRepository;
        }

        /// <summary>
        /// Запрос и сохранение 2х токенов: TokenXau и TokenXsts
        /// </summary>
        /// <param name="tokenOAuthDTO"></param>
        /// <returns></returns>
        internal async Task ProcessTokens(TokenOAuth tokenOAuthDTO)
        {
            if (tokenOAuthDTO == null)
                throw new ArgumentNullException(nameof(tokenOAuthDTO));

            await _authRepository.SaveTokenAsync(tokenOAuthDTO);

            TokenXau tokenXau = await RequestAndSaveXauToken(tokenOAuthDTO);
            await RequestAndSaveXstsToken(tokenXau);
        }

        private async Task<TokenXau> RequestAndSaveXauToken(TokenOAuth tokenOAuth)
        {
            TokenXau tokenXau = await _authService.RequestXauToken(tokenOAuth)
                ?? throw new InvalidOperationException("Failed to retrieve XAU token.");

            await _authRepository.SaveTokenAsync(tokenXau);
            return tokenXau;
        }

        private async Task RequestAndSaveXstsToken(TokenXau tokenXau)
        {
            TokenXsts tokenXsts = await _authService.RequestXstsToken(tokenXau)
                ?? throw new InvalidOperationException("Failed to retrieve XSTS token.");

            await _authRepository.SaveTokenAsync(tokenXsts);
        }
    }
}
