using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class AuthenticationUseCase
    {
        private readonly IAuthenticationService _authService;
        private readonly IAuthenticationRepository _authRepository;
        private TokenOAuthDTO _tokenOAuth;
        private TokenXauDTO _tokenXau;
        private TokenXstsDTO _tokenXsts;

        public AuthenticationUseCase(IAuthenticationService authServXbl, IAuthenticationRepository authServDb)
        {
            _authService = authServXbl;
            _authRepository = authServDb;
        }

        /// <summary>
        /// Need authorizationCode
        /// </summary>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokens(string authorizationCode)
        {
            TokenOAuthDTO responseOAuth = await _authService.RequestOauth2Token(authorizationCode);

            await ProcessTokens(responseOAuth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenOAuth"></param>
        /// <returns></returns>
        public async Task RefreshTokens(string tokenOAuth)
        {
            TokenOAuthDTO responseOAuth = await _authService.RefreshOauth2Token(tokenOAuth);

            await ProcessTokens(responseOAuth);
        }

        private async Task ProcessTokens(TokenOAuthDTO responseOAuth)
        {
            _authRepository.Save(responseOAuth);
            TokenXauDTO tokenXauDTO = await _authService.RequestXauToken(responseOAuth);

            //if (responseOAuth.IsSuccessStatusCode)
            //{
            //    _tokenOAuth = await _authService.DeserializeJson<TokenOAuthDTO>(responseOAuth);

            //    _authRepository.Save(_tokenOAuth);

            //    HttpResponseMessage responseXau = await _authService.RequestXauToken(_tokenOAuth.AccessToken);

            //    if (responseXau.IsSuccessStatusCode)
            //    {
            //        _tokenXau = await _authService.DeserializeJson<TokenXauDTO>(responseXau);

            //        _authRepository.Save(_tokenXau);

            //        HttpResponseMessage responseXsts = await _authService.RequestXstsToken(_tokenXau.Token);

            //        if (responseXsts.IsSuccessStatusCode)
            //        {
            //            _tokenXsts = await _authService.DeserializeJson<TokenXstsDTO>(responseXsts);

            //            _authRepository.Save(_tokenXsts);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// True - дата истекла, False - не истекла
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsDateExperid()
        {
            DateTime? dateNow = DateTime.UtcNow;

            DateTime? dateDb = _authRepository.GetDateExpired();

            return dateNow > dateDb ? true : false;
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
