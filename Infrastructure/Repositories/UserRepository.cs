using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using XblApp.Domain;

namespace Infrastructure.Repositories
{
    public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
    {
        public async Task<(bool Success, string Error, string UserId)> CreateUserAsync(string gamertag, string email, string password)
        {
            ApplicationUser user = new() { UserName = gamertag, Email = email };
            IdentityResult? result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return (false, string.Join(",", result.Errors.Select(e => e.Description)), null);

            return (true, null, user.Id);
        }

        public async Task<UserInfo> FindByGamertagAsync(string gamertag)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(gamertag);
            if (user == null) return null;

            return new UserInfo
            {
                Id = user.Id,
                Email = user.Email!
            };
        }

        public async Task<bool> CheckPasswordAsync(UserInfo userInfo, string password)
        {
            var user = await userManager.FindByIdAsync(userInfo.Id);
            if (user == null) return false;

            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo userInfo)
        {
            var user = await userManager.FindByIdAsync(userInfo.Id);
            if (user == null) return new List<string>();

            return await userManager.GetRolesAsync(user);
        }
    }
}
