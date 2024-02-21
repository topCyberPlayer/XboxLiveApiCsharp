using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class AuthenticationServiceDb
    {
        private WebSiteContext _dbContext;

        public AuthenticationServiceDb(WebSiteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveToDb(string userId, TokenXstsModelXbl tokenXbl)
        {
            TokenXstsModelDb entityToUpdate = await _dbContext.TokenXsts.FirstOrDefaultAsync(x => x.AspNetUserId == userId);

            if (entityToUpdate == null)
            {
                _dbContext.TokenXsts.Add(new TokenXstsModelDb
                {
                    IssueInstant = tokenXbl.IssueInstant,
                    NotAfter = tokenXbl.NotAfter,
                    Token = tokenXbl.Token,

                    Xuid = tokenXbl.Xuid,
                    Userhash = tokenXbl.Userhash,
                    Gamertag = tokenXbl.Gamertag,
                    AgeGroup = tokenXbl.AgeGroup,
                    Privileges = tokenXbl.Privileges,
                    UserPrivileges = tokenXbl.UserPrivileges,
                });
            }
            else 
            {
                entityToUpdate.AspNetUserId = userId;
                entityToUpdate.IssueInstant = tokenXbl.IssueInstant;
                entityToUpdate.NotAfter = tokenXbl.NotAfter;
                entityToUpdate.Token = tokenXbl.Token;

                entityToUpdate.Xuid = tokenXbl.Xuid;
                entityToUpdate.Userhash = tokenXbl.Userhash;
                entityToUpdate.Gamertag = tokenXbl.Gamertag;
                entityToUpdate.AgeGroup = tokenXbl.AgeGroup;
                entityToUpdate.Privileges = tokenXbl.Privileges;
                entityToUpdate.UserPrivileges = tokenXbl.UserPrivileges;
            }

            await _dbContext.SaveChangesAsync();
        }

        internal async Task GetAuthorizationHeaderValue(string userId)
        {
            var result = await _dbContext.TokenXsts.
                Where(x => x.AspNetUserId == userId).
                Select(x => new
                {
                    x.Userhash,
                    x.Token
                }).FirstOrDefaultAsync();

            //return result;
        }
    }
}
