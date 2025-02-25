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
            return _context.XboxUserTokens
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();
        }

        public DateTime GetDateXauTokenExpired()
        {
            return _context.XboxLiveTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public DateTime GetDateXstsTokenExpired()
        {
            return _context.XboxUserTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public async Task<XboxOAuthToken> GetTokenOAuth()
        {
            return await _context.XboxOAuthTokens
                .FirstOrDefaultAsync();
        }

        public async Task SaveTokenAsync(XboxOAuthToken tokenXbl)
        {
            XboxOAuthToken? token = await _context.XboxOAuthTokens.FirstOrDefaultAsync(x => x.UserId == tokenXbl.UserId);

            if (token == null)
                await _context.XboxOAuthTokens.AddAsync(tokenXbl);
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
            XboxLiveToken? token = await _context.XboxLiveTokens.FirstOrDefaultAsync(x => x.UhsId == tokenXbl.UhsId);

            if (token == null)
                await _context.XboxLiveTokens.AddAsync(tokenXbl);
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
            XboxUserToken? token = await _context.XboxUserTokens.FirstOrDefaultAsync(x => x.Xuid == tokenXbl.Xuid);

            if (token == null)
                await _context.XboxUserTokens.AddAsync(tokenXbl);
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
