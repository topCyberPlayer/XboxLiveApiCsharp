namespace XblApp.Domain.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string UserId, IEnumerable<string> Errors)> CreateUserAsync(string gamertag, string email, string password);

        Task<string> LoginUserAsync(string gamertag, string password);
    }
}
