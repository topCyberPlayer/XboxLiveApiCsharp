namespace XblApp.Domain.Interfaces
{
    public interface IRegisterUserService
    {
        Task<(bool Success, string UserId, IEnumerable<string> Errors)> CreateUserAsync(string gamertag, string email, string password);
    }
}
