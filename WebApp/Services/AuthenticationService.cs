using System.Net.Http.Headers;
using WebApp.Models;

namespace WebApp.Services
{
    public class AuthenticationService
    {
        internal AuthenticationServiceXbl _authServXbl;
        internal AuthenticationServiceDb _authServDb;

        internal string _xServiceUrl;
        internal HttpClient _httpClient;

        internal TokenOAuthModelXbl _tokenOAuth;
        internal TokenXauModelXbl _tokenXau;
        internal TokenXstsModelXbl _tokenXsts;

        public AuthenticationService(IConfiguration configuration,
            HttpClient httpClient2, 
            AuthenticationServiceXbl authServXbl, 
            AuthenticationServiceDb authServDb)
        {
            _authServXbl = authServXbl;
            _authServDb = authServDb;
            _httpClient = httpClient2;
        }

        /// <summary>
        /// Need authorizationCode
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task RequestTokens(string userId, string authorizationCode)
        {
            HttpResponseMessage responseOAuth = await _authServXbl.RequestOauth2Token(authorizationCode);

            await ProcessTokens(responseOAuth, userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenOAuth"></param>
        /// <returns></returns>
        internal async Task RefreshTokens(string userId, string tokenOAuth)
        {
            HttpResponseMessage responseOAuth = await _authServXbl.RefreshOauth2Token(tokenOAuth);

            await ProcessTokens(responseOAuth, userId);
        }

        internal async Task ProcessTokens(HttpResponseMessage responseOAuth, string userId)
        {
            if (responseOAuth.IsSuccessStatusCode)
            {
                _tokenOAuth = await _authServXbl.ProcessRespone<TokenOAuthModelXbl>(responseOAuth);

                HttpResponseMessage responseXau = await _authServXbl.RequestXauToken(_tokenOAuth);

                if (responseXau.IsSuccessStatusCode)
                {
                    _tokenXau = await _authServXbl.ProcessRespone<TokenXauModelXbl>(responseXau);

                    HttpResponseMessage responseXsts = await _authServXbl.RequestXstsToken(_tokenXau);

                    if (responseXsts.IsSuccessStatusCode)
                    {
                        _tokenXsts = await _authServXbl.ProcessRespone<TokenXstsModelXbl>(responseXsts);

                        _authServDb.SaveToDb(userId, _tokenXsts);
                    }
                }
            }
        }

        /// <summary>
        /// True - дата истекла, False - не истекла
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal bool IsDateExperid(string userId)
        {
            DateTime? dateNow = DateTime.UtcNow;

            DateTime? dateDb = _authServDb.IsDateExpired(userId);

            return dateNow>dateDb ? true : false;
        }

        public virtual async Task<T> GetBaseMethod<T>(string userId, string requestUri)
        {
            T result = default(T);

            if (IsDateExperid(userId))
            {
                string refreshToken = _authServDb.GetRefreshToken(userId);

                await RefreshTokens(userId, refreshToken);
            }

            string authorizationCode = _authServDb.GetAuthorizationHeaderValue(userId);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationCode);

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<T>();
            }

            return result;
        }
    }
}
