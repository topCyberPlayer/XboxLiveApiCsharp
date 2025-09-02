using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
    {
        public async Task<(bool Success, string Error, string UserId)> CreateUserAsync(string gamertag, string email, string password)
        {
            ApplicationUser user = new() { UserName = gamertag, Email = email };
            IdentityResult? createResult = await userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
                return (false, string.Join(",", createResult.Errors.Select(e => e.Description)), null);

            return (true, null, user.Id);
        }

        public async Task<(bool Success, string Error)> AddRoleToUserAsync(string userId, RoleType roleType)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            IdentityResult roleResult = await userManager.AddToRoleAsync(user, roleType.ToString());

            if (!roleResult.Succeeded)
                return (false, string.Join(",", roleResult.Errors.Select(e => e.Description)));

            return (true, null);
        }

        public async Task<UserInfo> FindByGamertagAsync(string gamertag)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(gamertag);
            if (user is null) 
                return null;

            return new UserInfo() {Id = user.Id, Email = user.Email };
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user == null) 
                return false;

            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user == null) 
                return new List<string>();

            return await userManager.GetRolesAsync(user);
        }
    }
}
