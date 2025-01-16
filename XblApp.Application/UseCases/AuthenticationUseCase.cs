using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application.UseCases
{
    public class AuthenticationUseCase : BaseUseCase
    {
        public AuthenticationUseCase(
            IAuthenticationRepository authRepository,
            IAuthenticationService authService) : base(authService, authRepository) { }

        /// <summary>
        /// Использовать когда OAuthToken вообще нет.
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokens(string authorizationCode)
        {
            TokenOAuth responseOAuth = await _authService.RequestOauth2Token(authorizationCode);

            await ProcessTokens(responseOAuth);
        }

        public string GenerateAuthorizationUrl()
        {
            string result = _authService.GenerateAuthorizationUrl();
            return result;
        }
    }
}
