using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Services.ProfileUser;

namespace WebApp.Services.Authentication
{
    public class AuthenticationProviderDb
    {
        private WebAppDbContext _dbContext;

        public AuthenticationProviderDb(WebAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> SaveToDb(TokenOauth2Response tokenOauth2)
        {
            _dbContext.TokenOauth2s.Update(new TokenOauth2ModelDb
            {
                TokenType = tokenOauth2.TokenType,
                ExpiresIn = tokenOauth2.ExpiresIn,
                Scope = tokenOauth2.Scope,
                AccessToken = tokenOauth2.AccessToken,
                RefreshToken = tokenOauth2.RefreshToken,
                AuthenticationToken = tokenOauth2.AuthenticationToken,
                UserId = tokenOauth2.UserId,
                
                Issued = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(tokenOauth2.ExpiresIn),
            });
            int number = await _dbContext.SaveChangesAsync();

            return number;
        }

        public async Task<int> SaveToDb(ProfileUserModelJson profileUser)
        {
            _dbContext.ProfileUsers.Update(new ProfileUserModelDb
            {
                ProfileUserId = long.Parse(profileUser.ProfileId),
                Gamerscore = profileUser.Gamerscore,
                DateTimeUpdate = DateTime.UtcNow,
                Bio = profileUser.Bio
            });
            int number = await _dbContext.SaveChangesAsync();

            return number;
        }

        internal Task<TokenOauth2ModelDb?> GetTokenOauth2(string userId)
        {
            return _dbContext.TokenOauth2s.FirstOrDefaultAsync(a => a.AspNetUserId == userId);
        }

        internal Task<TokenXstsModelDb?> GetTokenXsts(string userId)
        {
            return _dbContext.TokenXsts.FirstOrDefaultAsync(a => a.AspNetUserId == userId);
        }

        internal Task<ProfileUserModelDb?> GetProfileUser(string userId)
        {
            return _dbContext.ProfileUsers.FirstOrDefaultAsync(u => u.AspNetUsersId == userId);
        }
    }
}
