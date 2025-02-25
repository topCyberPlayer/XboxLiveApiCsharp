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

        public DateTime GetDateLiveTokenExpired()
        {
            return _context.XboxLiveTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public DateTime GetDateUserTokenExpired()
        {
            return _context.XboxUserTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public async Task<XboxOAuthToken> GetTokenAuth()
        {
            return await _context.XboxOAuthTokens
                .FirstOrDefaultAsync();
        }

        public async Task SaveTokensAsync(XboxOAuthToken authToken, XboxLiveToken liveToken, XboxUserToken userToken)
        {
            var existingAuthToken = await _context.XboxOAuthTokens
                .Include(x => x.XboxLiveTokenLink)
                .ThenInclude(x => x!.UserTokenLink)
                .FirstOrDefaultAsync(x => x.UserId == authToken.UserId);

            if (existingAuthToken == null)
            {
                authToken.XboxLiveTokenLink = liveToken;
                liveToken.UserTokenLink = userToken;
                await _context.XboxOAuthTokens.AddAsync(authToken);
            }
            else
            {
                // Обновляем данные OAuth токена
                existingAuthToken.AccessToken = authToken.AccessToken;
                existingAuthToken.RefreshToken = authToken.RefreshToken;
                existingAuthToken.ExpiresIn = authToken.ExpiresIn;
                existingAuthToken.AuthenticationToken = authToken.AuthenticationToken;

                if (existingAuthToken.XboxLiveTokenLink == null)
                {
                    existingAuthToken.XboxLiveTokenLink = liveToken;
                }
                else
                {
                    existingAuthToken.XboxLiveTokenLink.Token = liveToken.Token;
                    existingAuthToken.XboxLiveTokenLink.NotAfter = liveToken.NotAfter;
                }

                if (existingAuthToken.XboxLiveTokenLink!.UserTokenLink == null)
                {
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink = userToken;
                }
                else
                {
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.Token = userToken.Token;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.Gamertag = userToken.Gamertag;
                }
            }

            await _context.SaveChangesAsync();
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
