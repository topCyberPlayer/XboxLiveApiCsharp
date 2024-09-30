using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class AuthenticationUseCase : BaseUseCase
    {
        private TokenOAuthDTO _tokenOAuth;
        private TokenXauDTO _tokenXau;
        private TokenXstsDTO _tokenXsts;

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
            TokenOAuthDTO responseOAuth = await _authService.RequestOauth2Token(authorizationCode);

            await ProcessTokens(responseOAuth);
        }

            

        public string GenerateAuthorizationUrl()
        {
            string result = _authService.GenerateAuthorizationUrl();
            return result;
        }

        //public virtual async Task<T> GetBaseMethod<T>(string userId, string requestUri)
        //{
        //    T result = default(T);

        //    if (IsDateExperid(userId))
        //    {
        //        string refreshToken = _authRepository.GetRefreshToken(userId);

        //        await RefreshTokens(userId, refreshToken);
        //    }

        //    string authorizationCode = _authRepository.GetAuthorizationHeaderValue(userId);

        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationCode);

        //    HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        result = await response.Content.ReadFromJsonAsync<T>();
        //    }

        //    return result;
        //}
    }
}
