using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly XblAppDbContext _context;
        private readonly string? _userId;

        public AuthenticationRepository(XblAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task SaveAsync(TokenOAuth token)
        {
            if(token == null || _userId == null)
                return;

            TokenOAuth entityToUpdate = await _context.OAuthTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenOAuth()
                {
                    AspNetUserId = _userId,
                    AccessToken = token.AccessToken,
                    AuthenticationToken = token.AuthenticationToken,
                    ExpiresIn = token.ExpiresIn,
                    RefreshToken = token.RefreshToken,
                    TokenType = token.TokenType,
                    UserId = token.UserId,
                    Scope = token.Scope
                };

                await _context.OAuthTokens.AddAsync(entityToUpdate);
            }
            else
            {
                entityToUpdate.AccessToken = token.AccessToken;
                entityToUpdate.AuthenticationToken = token.AuthenticationToken;
                entityToUpdate.ExpiresIn = token.ExpiresIn;
                entityToUpdate.RefreshToken = token.RefreshToken;
                entityToUpdate.TokenType = token.TokenType;
                entityToUpdate.UserId = token.UserId;
                entityToUpdate.Scope = token.Scope;
            }

           await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(TokenXau token)
        {
            if (token == null || _userId == null)
                return;

            TokenXau entityToUpdate = await _context.XauTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXau()
                {
                    AspNetUserId = _userId,
                    IssueInstant = token.IssueInstant,
                    NotAfter = token.NotAfter,
                    Token = token.Token,
                    Uhs = token.Uhs
                };

                await _context.XauTokens.AddAsync(entityToUpdate);
            }

            else
            {
                entityToUpdate.IssueInstant = token.IssueInstant;
                entityToUpdate.NotAfter = token.NotAfter;
                entityToUpdate.Token = token.Token;
                entityToUpdate.Uhs = token.Uhs;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(TokenXsts token)
        {
            if (token == null || _userId == null)
                return;

            TokenXsts entityToUpdate = await _context.XstsTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXsts
                {
                    AspNetUserId = _userId,
                    IssueInstant = token.IssueInstant,
                    NotAfter = token.NotAfter,
                    Token = token.Token,

                    Xuid = token.Xuid,
                    Userhash = token.Userhash,
                    Gamertag = token.Gamertag,
                    AgeGroup = token.AgeGroup,
                    Privileges = token.Privileges,
                    UserPrivileges = token.UserPrivileges,
                };

                await _context.XstsTokens.AddAsync(entityToUpdate);
            }
            else
            {
                entityToUpdate.IssueInstant = token.IssueInstant;
                entityToUpdate.NotAfter = token.NotAfter;
                entityToUpdate.Token = token.Token;

                entityToUpdate.Xuid = token.Xuid;
                entityToUpdate.Userhash = token.Userhash;
                entityToUpdate.Gamertag = token.Gamertag;
                entityToUpdate.AgeGroup = token.AgeGroup;
                entityToUpdate.Privileges = token.Privileges;
                entityToUpdate.UserPrivileges = token.UserPrivileges;
            }

            await _context.SaveChangesAsync();
        }

        public string GetAuthorizationHeaderValue()
        {
            string? result = _context.XstsTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => $"XBL3.0 x={x.Userhash};{x.Token}")
                .FirstOrDefault();

            return result;
        }

        public DateTime GetDateExpired()
        {
            DateTime result = _context.XstsTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => x.NotAfter)
                .FirstOrDefault();

            return result;
        }

        public string GetRefreshToken()
        {
            string result = _context.OAuthTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => x.RefreshToken)
                .FirstOrDefault();

            return result;
        }
    }
}
