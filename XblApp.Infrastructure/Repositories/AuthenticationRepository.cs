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

        //todo Есть ошибка при обновлении токенов. Ощущение что какой-то токен не обновляется в БД.
        public async Task SaveOrUpdateTokensAsync(XboxOAuthToken authToken, XboxLiveToken liveToken, XboxUserToken userToken)
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
                existingAuthToken.AuthenticationToken = authToken.AuthenticationToken;
                existingAuthToken.ExpiresIn = authToken.ExpiresIn;

                if (existingAuthToken.XboxLiveTokenLink == null)
                {
                    existingAuthToken.XboxLiveTokenLink = liveToken;
                }
                else
                {
                    // Обновляем данные Live токена
                    existingAuthToken.XboxLiveTokenLink.IssueInstant = liveToken.IssueInstant;
                    existingAuthToken.XboxLiveTokenLink.NotAfter = liveToken.NotAfter;
                    existingAuthToken.XboxLiveTokenLink.Token = liveToken.Token;
                    
                }

                if (existingAuthToken.XboxLiveTokenLink!.UserTokenLink == null)
                {
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink = userToken;
                }
                else
                {
                    // Обновляем данные OAuth токена
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.IssueInstant = userToken.IssueInstant;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.NotAfter = userToken.NotAfter;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.Token = userToken.Token;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.Userhash = userToken.Userhash;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.Gamertag = userToken.Gamertag;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.AgeGroup = userToken.AgeGroup;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.Privileges = userToken.Privileges;
                    existingAuthToken.XboxLiveTokenLink.UserTokenLink.UserPrivileges = userToken.Privileges;

                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
