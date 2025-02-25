using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(XblAppDbContext context) : base(context)
        {
        }

        public Task<List<Gamer>> GetAllDonorsAsync()
        {
            throw new NotImplementedException();
        }

        public string? GetAuthorizationHeaderValue()
        {
            return _context.XstsTokens
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();
        }

        public DateTime GetDateXauTokenExpired()
        {
            return _context.XauTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public DateTime GetDateXstsTokenExpired()
        {
            return _context.XstsTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public async Task<XboxOAuthToken> GetTokenOAuth()
        {
            return await _context.OAuthTokens
                .FirstOrDefaultAsync();
        }

        public async Task SaveTokenAsync(XboxOAuthToken tokenXbl)
        {
            XboxOAuthToken? token = await _context.OAuthTokens.FirstOrDefaultAsync(x => x.UserId == tokenXbl.UserId);

            if (token == null)
                await _context.OAuthTokens.AddAsync(tokenXbl);
            else
            {
                token.AccessToken = tokenXbl.AccessToken;
                token.RefreshToken = tokenXbl.RefreshToken;
                token.AuthenticationToken = tokenXbl.AuthenticationToken;
                token.DateOfIssue = tokenXbl.DateOfIssue;
                token.DateOfExpiry = tokenXbl.DateOfExpiry;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(XboxLiveToken tokenXbl)
        {
            XboxLiveToken? token = await _context.XauTokens.FirstOrDefaultAsync(x => x.UhsId == tokenXbl.UhsId);

            if (token == null)
                await _context.XauTokens.AddAsync(tokenXbl);
            else
            {
                token.Token = tokenXbl.Token;
                token.IssueInstant = tokenXbl.IssueInstant;
                token.NotAfter = tokenXbl.NotAfter;
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(XboxUserToken tokenXbl)
        {
            XboxUserToken? token = await _context.XstsTokens.FirstOrDefaultAsync(x => x.Xuid == tokenXbl.Xuid);

            if (token == null)
                await _context.XstsTokens.AddAsync(tokenXbl);
            else
            {
                token.Token = tokenXbl?.Token;
                token.IssueInstant = tokenXbl.IssueInstant;
                token.NotAfter = tokenXbl.NotAfter;
                token.Gamertag = tokenXbl?.Gamertag;
            }

            await _context.SaveChangesAsync();
        }
    }
}
