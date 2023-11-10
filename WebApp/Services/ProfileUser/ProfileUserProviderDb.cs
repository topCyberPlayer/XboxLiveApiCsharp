using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Pages.Profiles;

namespace WebApp.Services.ProfileUser
{
    public class ProfileUserProviderDb
    {
        private WebAppDbContext _context;

        public ProfileUserProviderDb(WebAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfilesViewModel>> GetUserProfiles()
        {
            var userProfiles = await _context.ProfileUsers
                .Select(x => new UserProfilesViewModel
                {
                    GamerTag = x.Gamertag,
                    Gamerscore = x.Gamerscore,
                    LastDateTimeUpdate = x.DateTimeUpdate
                })
                .ToListAsync();

            return userProfiles;
        }
    }
}
