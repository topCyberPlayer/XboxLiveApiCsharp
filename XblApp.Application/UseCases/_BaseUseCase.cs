using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
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
        public async Task RefreshTokens(TokenOAuthDTO expiredTokenOAuth)
        {
            TokenOAuthDTO freshTokeneOAuth = await _authService.RefreshOauth2Token(expiredTokenOAuth);

            await ProcessTokens(freshTokeneOAuth);
        }

        /// <summary>
        /// Запрос и сохранение 2х токенов: TokenXau и TokenXsts
        /// </summary>
        /// <param name="tokenOAuthDTO"></param>
        /// <returns></returns>
        internal async Task ProcessTokens(TokenOAuthDTO tokenOAuthDTO)
        {
            if (tokenOAuthDTO != null)
            {
                await _authRepository.SaveAsync(tokenOAuthDTO);

                TokenXauDTO tokenXauDTO = await _authService.RequestXauToken(tokenOAuthDTO);

                if (tokenXauDTO != null)
                {
                    await _authRepository.SaveAsync(tokenXauDTO);

                    TokenXstsDTO responseXsts = await _authService.RequestXstsToken(tokenXauDTO);

                    if (responseXsts != null)
                    {
                        await _authRepository.SaveAsync(responseXsts);
                    }
                }
            }
        }
    }
}
