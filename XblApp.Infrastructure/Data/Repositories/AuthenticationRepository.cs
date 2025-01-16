using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.DTO;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class AuthenticationRepository : BaseService, IAuthenticationRepository
    {
        internal readonly string? _userId;
        public AuthenticationRepository(XblAppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _userId = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task SaveAsync(TokenOAuthDTO tokenDTO)
        {
            if(tokenDTO == null || _userId == null)
                return;

            TokenOAuth token = await _context.OAuthTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _userId);

            if (token == null)
            {
                token = new TokenOAuth()
                {
                    AspNetUserId = _userId,
                    AccessToken = tokenDTO.AccessToken,
                    AuthenticationToken = tokenDTO.AuthenticationToken,
                    ExpiresIn = tokenDTO.ExpiresIn,
                    RefreshToken = tokenDTO.RefreshToken,
                    TokenType = tokenDTO.TokenType,
                    UserId = tokenDTO.UserId,
                    Scope = tokenDTO.Scope
                };

                await _context.OAuthTokens.AddAsync(token);
            }
            else
            {
                token.AccessToken = tokenDTO.AccessToken;
                token.AuthenticationToken = tokenDTO.AuthenticationToken;
                token.ExpiresIn = tokenDTO.ExpiresIn;
                token.RefreshToken = tokenDTO.RefreshToken;
                token.TokenType = tokenDTO.TokenType;
                token.UserId = tokenDTO.UserId;
                token.Scope = tokenDTO.Scope;
            }

           await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(TokenXauDTO tokenDTO)
        {
            if (tokenDTO == null || _userId == null)
                return;

            TokenXau token = await _context.XauTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _userId);

            if (token == null)
            {
                token = new TokenXau()
                {
                    AspNetUserId = _userId,
                    IssueInstant = tokenDTO.IssueInstant,
                    NotAfter = tokenDTO.NotAfter,
                    Token = tokenDTO.Token,
                    Uhs = tokenDTO.Uhs
                };

                await _context.XauTokens.AddAsync(token);
            }

            else
            {
                token.IssueInstant = tokenDTO.IssueInstant;
                token.NotAfter = tokenDTO.NotAfter;
                token.Token = tokenDTO.Token;
                token.Uhs = tokenDTO.Uhs;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(TokenXstsDTO tokenDTO)
        {
            if (tokenDTO == null || _userId == null)
                return;

            TokenXsts token = await _context.XstsTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _userId);

            if (token == null)
            {
                token = new TokenXsts
                {
                    AspNetUserId = _userId,
                    IssueInstant = tokenDTO.IssueInstant,
                    NotAfter = tokenDTO.NotAfter,
                    Token = tokenDTO.Token,

                    Xuid = tokenDTO.Xuid,
                    Userhash = tokenDTO.Userhash,
                    Gamertag = tokenDTO.Gamertag,
                    AgeGroup = tokenDTO.AgeGroup,
                    Privileges = tokenDTO.Privileges,
                    UserPrivileges = tokenDTO.UserPrivileges,
                };

                await _context.XstsTokens.AddAsync(token);
            }
            else
            {
                token.IssueInstant = tokenDTO.IssueInstant;
                token.NotAfter = tokenDTO.NotAfter;
                token.Token = tokenDTO.Token;

                token.Xuid = tokenDTO.Xuid;
                token.Userhash = tokenDTO.Userhash;
                token.Gamertag = tokenDTO.Gamertag;
                token.AgeGroup = tokenDTO.AgeGroup;
                token.Privileges = tokenDTO.Privileges;
                token.UserPrivileges = tokenDTO.UserPrivileges;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<TokenOAuthDTO> GetTokenOAuth()
        {
            TokenOAuth? token = await _context.OAuthTokens.Where(x => x.AspNetUserId == _userId).FirstOrDefaultAsync();

            TokenOAuthDTO tokenOAuthDTO = new TokenOAuthDTO()
            {
                AccessToken = token.AccessToken,
                AuthenticationToken = token.AuthenticationToken,
                ExpiresIn = token.ExpiresIn,
                RefreshToken = token.RefreshToken,
                Scope = token.Scope,
                TokenType = token.TokenType
            };

            return tokenOAuthDTO;
        }

        public string GetAuthorizationHeaderValue()
        {
            string? result = _context.XstsTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();

            return result;
        }

        public DateTime GetDateXstsTokenExpired()
        {
            DateTime result = _context.XstsTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => x.NotAfter)
                .FirstOrDefault();

            return result;
        }

        public DateTime GetDateXauTokenExpired()
        {
            DateTime result = _context.XauTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => x.NotAfter)
                .FirstOrDefault();

            return result;
        }
    }
}
