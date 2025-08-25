using Domain.Entities.JsonModels;
using Domain.Entities.XblAuth;
using Domain.Interfaces.IRepository;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TKey>?> GetAllDonorsAsync<TKey>(Expression<Func<XboxAuthToken, TKey>> expressionSelect)
        {
            return await context.XboxOAuthTokens
                .Select(expressionSelect)
                .ToListAsync();
        }

        public string? GetAuthorizationHeaderValue()
        {
            return context.XboxUserTokens
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();
        }

        public DateTime GetDateLiveTokenExpired()
        {
            return context.XboxLiveTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public DateTime GetDateUserTokenExpired()
        {
            return context.XboxUserTokens
                .Select(a => a.NotAfter)
                .FirstOrDefault();
        }

        public async Task<XboxAuthToken?> GetXboxAuthToken() =>
            await context.XboxOAuthTokens.FirstOrDefaultAsync();


        public async Task SaveOrUpdateTokensAsync(
            OAuthTokenJson authTokenJson, XauTokenJson xauTokenJson, XstsTokenJson xstsTokenJson)
        {
            XboxAuthToken? existingAuthToken = await context.XboxOAuthTokens
                .Include(x => x.XboxXauTokenLink)
                .ThenInclude(x => x!.XboxXstsTokenLink)
                .FirstOrDefaultAsync(x => x.UserId == authTokenJson.UserId);

            if (existingAuthToken == null)
            {
                XboxAuthToken authToken = new()
                {
                    AccessToken = authTokenJson.AccessToken,
                    AuthenticationToken = authTokenJson.AuthenticationToken,
                    ExpiresIn = authTokenJson.ExpiresIn,
                    RefreshToken = authTokenJson.RefreshToken,
                    Scope = authTokenJson.Scope,
                    TokenType = authTokenJson.TokenType,
                    UserId = authTokenJson.UserId,
                    XboxXauTokenLink = new()
                    {
                        IssueInstant = xauTokenJson.IssueInstant,
                        NotAfter = xauTokenJson.NotAfter,
                        Token = xauTokenJson?.Token,
                        UhsId = xauTokenJson.Uhs,
                        XboxXstsTokenLink = new()
                        {
                            Gamertag = xstsTokenJson?.Gamertag,
                            Userhash = xstsTokenJson?.Userhash,
                            Token = xstsTokenJson?.Token,
                            AgeGroup = xstsTokenJson?.AgeGroup,
                            IssueInstant = xstsTokenJson.IssueInstant,
                            NotAfter = xstsTokenJson.NotAfter,
                            Privileges = xstsTokenJson?.Privileges,
                            Xuid = xstsTokenJson?.Xuid,
                            UserPrivileges = xstsTokenJson?.UserPrivileges,
                        }
                    }
                };

                await context.XboxOAuthTokens.AddAsync(authToken);
            }
            else
            {
                // Обновляем данные OAuth токена
                existingAuthToken.AccessToken = authTokenJson.AccessToken;
                existingAuthToken.RefreshToken = authTokenJson.RefreshToken;
                existingAuthToken.AuthenticationToken = authTokenJson.AuthenticationToken;
                existingAuthToken.ExpiresIn = authTokenJson.ExpiresIn;

                if (existingAuthToken.XboxXauTokenLink == null)
                {
                    existingAuthToken.XboxXauTokenLink = new()
                    {
                        IssueInstant = xauTokenJson.IssueInstant,
                        NotAfter = xauTokenJson.NotAfter,
                        Token = xauTokenJson?.Token,
                        UhsId = xauTokenJson.Uhs,
                        XboxXstsTokenLink = new()
                        {
                            Gamertag = xstsTokenJson?.Gamertag,
                            Userhash = xstsTokenJson?.Userhash,
                            Token = xstsTokenJson?.Token,
                            AgeGroup = xstsTokenJson?.AgeGroup,
                            IssueInstant = xstsTokenJson.IssueInstant,
                            NotAfter = xstsTokenJson.NotAfter,
                            Privileges = xstsTokenJson?.Privileges,
                            Xuid = xstsTokenJson?.Xuid,
                            UserPrivileges = xstsTokenJson?.UserPrivileges,
                        }
                    };
                }
                else
                {
                    // Обновляем данные Live токена
                    existingAuthToken.XboxXauTokenLink.IssueInstant = xauTokenJson.IssueInstant;
                    existingAuthToken.XboxXauTokenLink.NotAfter = xauTokenJson.NotAfter;
                    existingAuthToken.XboxXauTokenLink.Token = xauTokenJson.Token;

                }

                if (existingAuthToken.XboxXauTokenLink!.XboxXstsTokenLink == null)
                {
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink = new()
                    {
                        Gamertag = xstsTokenJson?.Gamertag,
                        Userhash = xstsTokenJson?.Userhash,
                        Token = xstsTokenJson?.Token,
                        AgeGroup = xstsTokenJson?.AgeGroup,
                        IssueInstant = xstsTokenJson.IssueInstant,
                        NotAfter = xstsTokenJson.NotAfter,
                        Privileges = xstsTokenJson?.Privileges,
                        Xuid = xstsTokenJson?.Xuid,
                        UserPrivileges = xstsTokenJson?.UserPrivileges,
                    };
                }
                else
                {
                    // Обновляем данные OAuth токена
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.IssueInstant = xstsTokenJson.IssueInstant;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.NotAfter = xstsTokenJson.NotAfter;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.Token = xstsTokenJson.Token;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.Userhash = xstsTokenJson.Userhash;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.Gamertag = xstsTokenJson.Gamertag;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.AgeGroup = xstsTokenJson.AgeGroup;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.Privileges = xstsTokenJson.Privileges;
                    existingAuthToken.XboxXauTokenLink.XboxXstsTokenLink.UserPrivileges = xstsTokenJson.Privileges;

                }
            }

            await context.SaveChangesAsync();
        }
    }
}
