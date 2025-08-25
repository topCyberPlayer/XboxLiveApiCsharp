using XblApp.Domain;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<(bool Success, string Error, string UserId)> CreateUserAsync(string gamertag, string email, string password);
        Task<UserInfo> FindByGamertagAsync(string gamertag);
        Task<bool> CheckPasswordAsync(UserInfo user, string password);
        Task<IList<string>> GetRolesAsync(UserInfo user);
    }
}
