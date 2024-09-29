using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using XblApp.Domain.Entities;

namespace XblApp.Application.UseCases
{
    public class AuthenticationUseCase
    {
        private readonly IAuthenticationService _authService;
        private readonly IAuthenticationRepository _authRepository;
        private TokenOAuthDTO _tokenOAuth;
        private TokenXauDTO _tokenXau;
        private TokenXstsDTO _tokenXsts;
        private readonly string? _userId;

        public AuthenticationUseCase()
        {
            
        }

        public AuthenticationUseCase(
            IAuthenticationService authServXbl, 
            IAuthenticationRepository authServDb,
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = authServXbl;
            _authRepository = authServDb;
            _userId = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
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


        /// <summary>
        /// Вызывается из 
        /// </summary>
        /// <param name="tokenOAuthDTO"></param>
        /// <returns></returns>
        private async Task ProcessTokens(TokenOAuthDTO tokenOAuthDTO)
        {
            if (tokenOAuthDTO != null)
            {
                await _authRepository.SaveAsync(new TokenOAuth
                {
                    UserId = tokenOAuthDTO.UserId,
                    TokenType = tokenOAuthDTO.TokenType,
                    ExpiresIn = tokenOAuthDTO.ExpiresIn,
                    Scope = tokenOAuthDTO.Scope,
                    AccessToken = tokenOAuthDTO.AccessToken,
                    RefreshToken = tokenOAuthDTO.RefreshToken,
                    AuthenticationToken = tokenOAuthDTO.AuthenticationToken,
                    AspNetUserId = _userId,
                });

                TokenXauDTO tokenXauDTO = await _authService.RequestXauToken(tokenOAuthDTO);

                if (tokenXauDTO != null)
                {
                    await _authRepository.SaveAsync(new TokenXau
                    {
                        Uhs = tokenXauDTO.Uhs,
                        IssueInstant = tokenXauDTO.IssueInstant,
                        NotAfter = tokenXauDTO.NotAfter,
                        Token = tokenXauDTO.Token,
                        AspNetUserId = _userId
                    });

                    TokenXstsDTO responseXsts = await _authService.RequestXstsToken(tokenXauDTO);

                    if (responseXsts != null)
                    {
                        await _authRepository.SaveAsync(new TokenXsts
                        {
                            Xuid = responseXsts.Xuid,
                            Userhash = responseXsts.Userhash,
                            Gamertag = responseXsts.Gamertag,
                            AgeGroup = responseXsts.AgeGroup,
                            Privileges = responseXsts.Privileges,
                            UserPrivileges = responseXsts.UserPrivileges,
                            IssueInstant = responseXsts.IssueInstant,
                            NotAfter = responseXsts.NotAfter,
                            Token = responseXsts.Token,
                            AspNetUserId = _userId
                        });
                    }
                }
            }
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
