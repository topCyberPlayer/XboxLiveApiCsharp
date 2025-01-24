using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        private const string AspNetUserId = "14111a1d-0ce5-485a-b032-987e7f10ec72";

        public AuthenticationRepository(XblAppDbContext context) : base(context)
        {
        }

        public string GetAuthorizationHeaderValue()
        {
            return _context.XstsTokens
                .Where(x => x.AspNetUserId == AspNetUserId)
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();
        }

        public DateTime GetDateXauTokenExpired()
        {
            return _context.XauTokens
                .Where(a => a.AspNetUserId == AspNetUserId)
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public DateTime GetDateXstsTokenExpired()
        {
            return _context.XstsTokens
                .Where(a => a.AspNetUserId == AspNetUserId)
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public async Task<TokenOAuth> GetTokenOAuth()
        {
            return await _context.OAuthTokens
                .Where(a => a.AspNetUserId == AspNetUserId)
                .FirstOrDefaultAsync();
        }

        public async Task SaveTokenAsync(TokenOAuth tokenXbl)
        {
            TokenOAuth token = await _context.OAuthTokens.FirstOrDefaultAsync(x => x.AspNetUserId == AspNetUserId);

            if (token == null)
            {
                await _context.OAuthTokens.AddAsync(tokenXbl);
            }
            else
            {
                token.AccessToken = tokenXbl.AccessToken;
                token.AuthenticationToken = tokenXbl.AuthenticationToken;
                token.ExpiresIn = tokenXbl.ExpiresIn;
                token.RefreshToken = tokenXbl.RefreshToken;
                token.TokenType = tokenXbl.TokenType;
                token.UserId = tokenXbl.UserId;
                token.Scope = tokenXbl.Scope;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(TokenXau tokenXbl)
        {
            TokenXau token = await _context.XauTokens.FirstOrDefaultAsync(x => x.AspNetUserId == AspNetUserId);

            if (token == null)
            {
                await _context.XauTokens.AddAsync(tokenXbl);
            }

            else
            {
                token.IssueInstant = tokenXbl.IssueInstant;
                token.NotAfter = tokenXbl.NotAfter;
                token.Token = tokenXbl.Token;
                token.Uhs = tokenXbl.Uhs;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(TokenXsts tokenXbl)
        {
            TokenXsts token = await _context.XstsTokens.FirstOrDefaultAsync(x => x.AspNetUserId == AspNetUserId);

            if (token == null)
            {
                await _context.XstsTokens.AddAsync(tokenXbl);
            }
            else
            {
                token.IssueInstant = tokenXbl.IssueInstant;
                token.NotAfter = tokenXbl.NotAfter;
                token.Token = tokenXbl.Token;

                token.Xuid = tokenXbl.Xuid;
                token.Userhash = tokenXbl.Userhash;
                token.Gamertag = tokenXbl.Gamertag;
                token.AgeGroup = tokenXbl.AgeGroup;
                token.Privileges = tokenXbl.Privileges;
                token.UserPrivileges = tokenXbl.UserPrivileges;
            }

            await _context.SaveChangesAsync();
        }
    }
}
