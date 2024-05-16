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

        public void SaveToDb(string userId, TokenOAuthModelXbl tokenXbl)
        {
            TokenOAuthModelDb entityToUpdate = _dbContext.TokenOAuth.FirstOrDefault(x => x.AspNetUserId == userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenOAuthModelDb()
                {
                    AspNetUserId = userId,
                    AccessToken = tokenXbl.AccessToken,
                    AuthenticationToken = tokenXbl.AuthenticationToken,
                    ExpiresIn = tokenXbl.ExpiresIn,
                    RefreshToken = tokenXbl.RefreshToken,
                    TokenType = tokenXbl.TokenType,
                    UserId = tokenXbl.UserId,
                    Scope = tokenXbl.Scope
                };

                _dbContext.TokenOAuth.Add(entityToUpdate);
            }
            else
            {
                entityToUpdate.AccessToken = tokenXbl.AccessToken;
                entityToUpdate.AuthenticationToken = tokenXbl.AuthenticationToken;
                entityToUpdate.ExpiresIn = tokenXbl.ExpiresIn;
                entityToUpdate.RefreshToken = tokenXbl.RefreshToken;
                entityToUpdate.TokenType = tokenXbl.TokenType;
                entityToUpdate.UserId = tokenXbl.UserId;
                entityToUpdate.Scope = tokenXbl.Scope;
            }

            _dbContext.SaveChanges();
        }

        public void SaveToDb(string userId, TokenXauModelXbl tokenXbl)
        {
            TokenXauModelDb entityToUpdate = _dbContext.TokenXau.FirstOrDefault(x => x.AspNetUserId == userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXauModelDb()
                {
                    AspNetUserId = userId,
                    IssueInstant = tokenXbl.IssueInstant,
                    NotAfter = tokenXbl.NotAfter,
                    Token = tokenXbl.Token,
                    Uhs = tokenXbl.Uhs
                };

                _dbContext.TokenXau.Add(entityToUpdate);
            }

            else
            {
                entityToUpdate.IssueInstant = tokenXbl.IssueInstant;
                entityToUpdate.NotAfter = tokenXbl.NotAfter;
                entityToUpdate.Token = tokenXbl.Token;
                entityToUpdate.Uhs = tokenXbl.Uhs;
            }

            _dbContext.SaveChanges();
        }

        public void SaveToDb(string userId, TokenXstsModelXbl tokenXbl)
        {
            TokenXstsModelDb entityToUpdate =  _dbContext.TokenXsts.FirstOrDefault(x => x.AspNetUserId == userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXstsModelDb
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

                _dbContext.TokenXsts.Add(entityToUpdate);
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
            string? result = _dbContext.TokenXsts
                .Where(x => x.AspNetUserId == userId)
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();

            return result;
        }

        public DateTime GetDateExpired(string userId)
        {
            DateTime result = _dbContext.TokenXsts
                .Where(x => x.AspNetUserId == userId)
                .Select(x => x.NotAfter)
                .FirstOrDefault();

            return result;
        }

        internal string GetRefreshToken(string userId)
        {
            string result = _dbContext.TokenOAuth
                .Where(x => x.AspNetUserId == userId)
                .Select(x => x.RefreshToken)
                .FirstOrDefault();

            return result;                
        }
    }
}
