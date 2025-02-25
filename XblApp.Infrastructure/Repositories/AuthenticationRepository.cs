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

        public async Task<List<(string UserId, DateTime XboxLiveNotAfter, DateTime XboxUserNotAfter, string Xuid, string Gamertag)>?>
        GetAllDonorsAsync()
        {
            var result = await (from oauth in _context.XboxOAuthTokens
                                join live in _context.XboxLiveTokens on oauth.UserId equals live.UserIdFK
                                join user in _context.XboxUserTokens on live.UhsId equals user.UhsIdFK
                                select new
                                {
                                    oauth.UserId,
                                    XboxLiveNotAfter = live.NotAfter,
                                    XboxUserNotAfter = user.NotAfter,
                                    user.Xuid,
                                    user.Gamertag
                                })
                                .ToListAsync();

            return result
                .Select(r => (r.UserId, r.XboxLiveNotAfter, r.XboxUserNotAfter, r.Xuid, r.Gamertag))
                .ToList();
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

        public async Task<XboxOAuthToken> GetXboxAuthToken()
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
    }
}
