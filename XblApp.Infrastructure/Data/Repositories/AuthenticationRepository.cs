using Microsoft.AspNetCore.Http;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly XblAppDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string? _userId;

        public AuthenticationRepository(XblAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
            _userId = httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        public void Save(TokenOAuthDTO tokenXbl)
        {
            TokenOAuth entityToUpdate = _context.OAuthTokens.FirstOrDefault(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenOAuth()
                {
                    AspNetUserId = _userId,
                    AccessToken = tokenXbl.AccessToken,
                    AuthenticationToken = tokenXbl.AuthenticationToken,
                    ExpiresIn = tokenXbl.ExpiresIn,
                    RefreshToken = tokenXbl.RefreshToken,
                    TokenType = tokenXbl.TokenType,
                    UserId = tokenXbl.UserId,
                    Scope = tokenXbl.Scope
                };

                _context.OAuthTokens.Add(entityToUpdate);
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

            _context.SaveChanges();
        }

        public void Save(TokenXauDTO tokenXbl)
        {
            TokenXau entityToUpdate = _context.XauTokens.FirstOrDefault(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXau()
                {
                    AspNetUserId = _userId,
                    IssueInstant = tokenXbl.IssueInstant,
                    NotAfter = tokenXbl.NotAfter,
                    Token = tokenXbl.Token,
                    Uhs = tokenXbl.Uhs
                };

                _context.XauTokens.Add(entityToUpdate);
            }

            else
            {
                entityToUpdate.IssueInstant = tokenXbl.IssueInstant;
                entityToUpdate.NotAfter = tokenXbl.NotAfter;
                entityToUpdate.Token = tokenXbl.Token;
                entityToUpdate.Uhs = tokenXbl.Uhs;
            }

            _context.SaveChanges();
        }

        public void Save(TokenXstsDTO tokenXbl)
        {
            TokenXsts entityToUpdate = _context.XstsTokens.FirstOrDefault(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXsts
                {
                    AspNetUserId = _userId,
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

                _context.XstsTokens.Add(entityToUpdate);
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

            _context.SaveChanges();
        }

        public string GetAuthorizationHeaderValue()
        {
            string? result = _context.XstsTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => $"x={x.Userhash};{x.Token}")
                .FirstOrDefault();

            return result;
        }

        public DateTime GetDateExpired()
        {
            DateTime result = _context.XstsTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => x.NotAfter)
                .FirstOrDefault();

            return result;
        }

        public string GetRefreshToken()
        {
            string result = _context.OAuthTokens
                .Where(x => x.AspNetUserId == _userId)
                .Select(x => x.RefreshToken)
                .FirstOrDefault();

            return result;
        }
    }
}
