using ProfileService.Data;
using ProfileService.Profiles;

namespace ProfileService.Services
{
    public class ProfileServiceDb
    {
        private ProfileContext _dbContext;
        public ProfileServiceDb(ProfileContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProfileModelDb GetProfileByGamertag(string gamertag)
        {
            ProfileModelDb? result = _dbContext.Profiles
                .Where(x => x.Gamertag == gamertag)
                .FirstOrDefault();

            return result;
        }
    }
}
