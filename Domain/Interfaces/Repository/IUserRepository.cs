using Domain.Enums;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<(bool Success, string Error, string UserId)> CreateUserAsync(string gamertag, string email, string password);
        Task<(bool Success, string Error)> AddRoleToUserAsync(string userId, RoleType roleType = RoleType.Gamer);
        Task<UserInfo> FindByGamertagAsync(string gamertag);
        Task<bool> CheckPasswordAsync(string userId, string password);
        Task<IList<string>> GetRolesAsync(string userId);
    }
}
