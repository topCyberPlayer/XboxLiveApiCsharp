using Domain.DTO;

namespace Domain.Interfaces
{
    public interface ITokenService
    {
        TokenDTO GenerateToken(string? userId, string? email, IList<string>? roles);
        (bool isValid, string gamertag) ValidateToken(string accessToken);
    }
}