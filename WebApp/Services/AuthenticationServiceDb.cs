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

        public void SaveToDb(string userId, TokenXstsModelXbl tokenXbl)
        {
            TokenXstsModelDb entityToUpdate =  _dbContext.TokenXsts.FirstOrDefault(x => x.AspNetUserId == userId);
            //todo Выяснить почему FirstOrDefaultAsync ничего не возвращает

            if (entityToUpdate == null)
            {
                TokenXstsModelDb token = new TokenXstsModelDb
                {
                    AspNetUserId = userId,
                    IssueInstant = tokenXbl.IssueInstant,
                    NotAfter = tokenXbl.NotAfter,
                    Token = tokenXbl.Token,

                    Xuid = tokenXbl.Xuid,
                    Userhash = tokenXbl.Userhash,
                    Gamertag = tokenXbl.Gamertag,
                    AgeGroup = tokenXbl.AgeGroup,
                    Privileges = tokenXbl.Privileges,
                    UserPrivileges = tokenXbl.UserPrivileges,
                };

                _dbContext.TokenXsts.Add(token);
            }
            else 
            {
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

            _dbContext.SaveChanges();
        }

        internal string GetAuthorizationHeaderValue(string userId)
        {
            string result = _dbContext.TokenXsts
                .Where(x => x.AspNetUserId == userId)
                .Select(x => $"XBL3.0 x={x.Userhash};{x.Token}")
                .FirstOrDefault();

            return result;
        }
    }
}
