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

        public void Save(TokenOAuth token)
        {
            TokenOAuth entityToUpdate = _context.OAuthTokens.FirstOrDefault(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenOAuth()
                {
                    AspNetUserId = _userId,
                    AccessToken = token.AccessToken,
                    AuthenticationToken = token.AuthenticationToken,
                    ExpiresIn = token.ExpiresIn,
                    RefreshToken = token.RefreshToken,
                    TokenType = token.TokenType,
                    UserId = token.UserId,
                    Scope = token.Scope
                };

                _context.OAuthTokens.Add(entityToUpdate);
            }
            else
            {
                entityToUpdate.AccessToken = token.AccessToken;
                entityToUpdate.AuthenticationToken = token.AuthenticationToken;
                entityToUpdate.ExpiresIn = token.ExpiresIn;
                entityToUpdate.RefreshToken = token.RefreshToken;
                entityToUpdate.TokenType = token.TokenType;
                entityToUpdate.UserId = token.UserId;
                entityToUpdate.Scope = token.Scope;
            }

            _context.SaveChanges();
        }

        public void Save(TokenXau token)
        {
            TokenXau entityToUpdate = _context.XauTokens.FirstOrDefault(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXau()
                {
                    AspNetUserId = _userId,
                    IssueInstant = token.IssueInstant,
                    NotAfter = token.NotAfter,
                    Token = token.Token,
                    Uhs = token.Uhs
                };

                _context.XauTokens.Add(entityToUpdate);
            }

            else
            {
                entityToUpdate.IssueInstant = token.IssueInstant;
                entityToUpdate.NotAfter = token.NotAfter;
                entityToUpdate.Token = token.Token;
                entityToUpdate.Uhs = token.Uhs;
            }

            _context.SaveChanges();
        }

        public void Save(TokenXsts token)
        {
            TokenXsts entityToUpdate = _context.XstsTokens.FirstOrDefault(x => x.AspNetUserId == _userId);

            if (entityToUpdate == null)
            {
                entityToUpdate = new TokenXsts
                {
                    AspNetUserId = _userId,
                    IssueInstant = token.IssueInstant,
                    NotAfter = token.NotAfter,
                    Token = token.Token,

                    Xuid = token.Xuid,
                    Userhash = token.Userhash,
                    Gamertag = token.Gamertag,
                    AgeGroup = token.AgeGroup,
                    Privileges = token.Privileges,
                    UserPrivileges = token.UserPrivileges,
                };

                _context.XstsTokens.Add(entityToUpdate);
            }
            else
            {
                entityToUpdate.IssueInstant = token.IssueInstant;
                entityToUpdate.NotAfter = token.NotAfter;
                entityToUpdate.Token = token.Token;

                entityToUpdate.Xuid = token.Xuid;
                entityToUpdate.Userhash = token.Userhash;
                entityToUpdate.Gamertag = token.Gamertag;
                entityToUpdate.AgeGroup = token.AgeGroup;
                entityToUpdate.Privileges = token.Privileges;
                entityToUpdate.UserPrivileges = token.UserPrivileges;
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
