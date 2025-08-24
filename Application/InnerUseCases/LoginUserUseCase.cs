using XblApp.Domain;
using XblApp.Domain.Interfaces;
using XblApp.Domain.Interfaces.Repository;
using XblApp.Domain.Responses;

namespace Application.InnerUseCases
{
    public class LoginUserUseCase(IUserRepository userRepository, ITokenService tokenService)
    {
        public async Task<LoginUserResult> LoginUser(string gamertag, string password)
        {
            UserInfo user = await userRepository.FindByGamertagAsync(gamertag);
            if (user == null)
                throw new UnauthorizedAccessException("Неверный gamertag иил пароль");

            bool valid = await userRepository.CheckPasswordAsync(user, password);
            if (!valid)
                throw new UnauthorizedAccessException("Неверный gamertag иил пароль");

            var roles = await userRepository.GetRolesAsync(user);

            return tokenService.GenerateToken(user.Id, user.Email, roles);
        }
    }
}