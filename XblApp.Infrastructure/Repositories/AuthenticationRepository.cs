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

        public async Task<TokenOAuth> GetTokenOAuth()
        {
            return await _context.OAuthTokens
                .FirstOrDefaultAsync();
        }

        public async Task SaveTokenAsync(TokenOAuth tokenXbl)
        {
            TokenOAuth? token = await _context.OAuthTokens.FirstOrDefaultAsync(x => x.AspNetUserId == tokenXbl.AspNetUserId);

            if (token == null)
                await _context.OAuthTokens.AddAsync(tokenXbl);
            else
                _context.OAuthTokens.Update(tokenXbl);

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(TokenXau tokenXbl)
        {
            TokenXau? token = await _context.XauTokens.FirstOrDefaultAsync(x => x.AspNetUserId == tokenXbl.AspNetUserId);

            if (token == null)
                await _context.XauTokens.AddAsync(tokenXbl);
            else
                _context.XauTokens.Update(tokenXbl);

            await _context.SaveChangesAsync();
        }

        public async Task SaveTokenAsync(TokenXsts tokenXbl)
        {
            TokenXsts? token = await _context.XstsTokens.FirstOrDefaultAsync(x => x.AspNetUserId == tokenXbl.AspNetUserId);

            if (token == null)
                await _context.XstsTokens.AddAsync(tokenXbl);
            else
                _context.XstsTokens.Update(tokenXbl);

            await _context.SaveChangesAsync();
        }
    }
}
