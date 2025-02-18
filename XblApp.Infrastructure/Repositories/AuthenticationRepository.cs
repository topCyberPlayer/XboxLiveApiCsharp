using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        private readonly string _aspNetUserId;

        public AuthenticationRepository(XblAppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _aspNetUserId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public Task<List<Gamer>> GetAllDonorsAsync()
        {
            throw new NotImplementedException();
        }

        public string GetAuthorizationHeaderValue()
        {
            return _context.XstsTokens
                .Where(x => x.AspNetUserId == _aspNetUserId)
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();
        }

        public DateTime GetDateXauTokenExpired()
        {
            return _context.XauTokens
                .Where(a => a.AspNetUserId == _aspNetUserId)
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public DateTime GetDateXstsTokenExpired()
        {
            return _context.XstsTokens
                .Where(a => a.AspNetUserId == _aspNetUserId)
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public async Task<TokenOAuth> GetTokenOAuth()
        {
            return await _context.OAuthTokens
                .Where(a => a.AspNetUserId == _aspNetUserId)
                .FirstOrDefaultAsync();
        }

        public async Task SaveTokenAsync(TokenOAuth tokenXbl)
        {
            TokenOAuth token = await _context.OAuthTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _aspNetUserId);

            if (token == null)
            {
                await _context.OAuthTokens.AddAsync(new TokenOAuth
                {
                    AspNetUserId = _aspNetUserId,
                    AccessToken = tokenXbl.AccessToken,
                    AuthenticationToken = tokenXbl.AuthenticationToken,
                    ExpiresIn = tokenXbl.ExpiresIn,
                    RefreshToken = tokenXbl.RefreshToken,
                    TokenType = tokenXbl.TokenType,
                    UserId  = tokenXbl.UserId,
                    Scope = tokenXbl.Scope
                });
            }
            else
            {
                _context.OAuthTokens.Update(tokenXbl);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(TokenXau tokenXbl)
        {
            TokenXau token = await _context.XauTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _aspNetUserId);

            if (token == null)
            {
                await _context.XauTokens.AddAsync(new TokenXau
                {
                    AspNetUserId = _aspNetUserId,
                    IssueInstant = tokenXbl.IssueInstant,
                    NotAfter = tokenXbl.NotAfter,
                    Token = tokenXbl.Token,
                    Uhs = tokenXbl.Uhs
                });
            }
            else
            {
                _context.XauTokens.Update(tokenXbl);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(TokenXsts tokenXbl)
        {
            TokenXsts token = await _context.XstsTokens.FirstOrDefaultAsync(x => x.AspNetUserId == _aspNetUserId);

            if (token == null)
            {
                await _context.XstsTokens.AddAsync(new TokenXsts
                {
                    AspNetUserId = _aspNetUserId,
                    IssueInstant = tokenXbl.IssueInstant,
                    NotAfter = tokenXbl.NotAfter,
                    Token = tokenXbl.Token,
                    AgeGroup = tokenXbl.AgeGroup,
                    Gamertag = tokenXbl.Gamertag,
                    Privileges = tokenXbl.Privileges,
                    Userhash = tokenXbl.Userhash,
                    UserPrivileges = tokenXbl.UserPrivileges,
                    Xuid = tokenXbl.Xuid,
                });
            }
            else
            {
                _context.XstsTokens.Update(tokenXbl);
            }

            await _context.SaveChangesAsync();
        }
    }
}
