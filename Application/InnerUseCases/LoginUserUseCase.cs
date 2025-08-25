using Domain.DTO;
using Domain.Interfaces;
using XblApp.Domain;
using Domain.Interfaces.Repository;

namespace Application.InnerUseCases
{
    public class LoginUserUseCase(IUserRepository userRepository, ITokenService tokenService)
    {
        public async Task<TokenDTO> LoginUser(string gamertag, string password)
        {
            UserInfo user = await userRepository.FindByGamertagAsync(gamertag);
            if (user == null)
                throw new UnauthorizedAccessException("Неверный gamertag иил пароль");

            bool valid = await userRepository.CheckPasswordAsync(user, password);
            if (!valid)
                throw new UnauthorizedAccessException("Неверный gamertag иил пароль");

            IList<string>? roles = await userRepository.GetRolesAsync(user);

            return tokenService.GenerateToken(user.Id, user.Email, roles);
        }

        public async Task<TokenDTO> RefreshTokenAsync(TokenDTO dto)
        {
            (bool isValid, string gamertag) = tokenService.ValidateToken(dto.AccessToken);

            if (!isValid)
                throw new UnauthorizedAccessException("Неверноеый Access token");

            UserInfo user = await userRepository.FindByGamertagAsync(gamertag);
            if (user == null)
                throw new UnauthorizedAccessException("Пользователь не найден");

            IList<string>? roles = await userRepository.GetRolesAsync(user);

            return tokenService.GenerateToken(user.Id, user.Email, roles);
        }
    }
}