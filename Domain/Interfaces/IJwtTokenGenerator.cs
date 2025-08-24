
using XblApp.Domain.Responses;

namespace XblApp.Domain.Interfaces
{
    public interface ITokenService
    {
        LoginUserResult GenerateToken(string? userId, string? email, IList<string>? roles);
    }
}