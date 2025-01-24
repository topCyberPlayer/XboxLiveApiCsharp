using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application
{
    public class BaseUseCase
    {
        internal readonly IAuthenticationService _authService;
        internal readonly IAuthenticationRepository _authRepository;

        public BaseUseCase(IAuthenticationService authService, IAuthenticationRepository authRepository)
        {
            _authService = authService;
            _authRepository = authRepository;
        }

        /// <summary>
        /// Использовать когда уже есть OAuthToken, но его срок истек.
        /// </summary>
        /// <param name="tokenOAuth"></param>
        /// <returns></returns>
        public async Task RefreshTokens(TokenOAuth expiredTokenOAuth)
        {
            TokenOAuth freshTokeneOAuth = await _authService.RefreshOauth2Token(expiredTokenOAuth);

            await ProcessTokens(freshTokeneOAuth);
        }

        /// <summary>
        /// Запрос и сохранение 2х токенов: TokenXau и TokenXsts
        /// </summary>
        /// <param name="tokenOAuthDTO"></param>
        /// <returns></returns>
        internal async Task ProcessTokens(TokenOAuth tokenOAuthDTO)
        {
            if (tokenOAuthDTO != null)
            {
                await _authRepository.SaveTokenAsync(tokenOAuthDTO);

                TokenXau tokenXau = await _authService.RequestXauToken(tokenOAuthDTO);

                if (tokenXau != null)
                {
                    await _authRepository.SaveTokenAsync(tokenXau);

                    TokenXsts responseXsts = await _authService.RequestXstsToken(tokenXau);

                    if (responseXsts != null)
                    {
                        await _authRepository.SaveTokenAsync(responseXsts);
                    }
                }
            }
        }
    }
}
