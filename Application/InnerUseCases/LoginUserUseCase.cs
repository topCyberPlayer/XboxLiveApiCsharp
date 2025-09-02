using Domain.DTO;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain;

namespace Application.InnerUseCases
{
    public class LoginUserUseCase(IUserRepository userRepository, ITokenService tokenService)
    {
        public async Task<TokenDTO> LoginUser(string gamertag, string password)
        {
            UserInfo userInfo = await userRepository.FindByGamertagAsync(gamertag);
            if (userInfo is null)
                throw new UnauthorizedAccessException($"Пользователь с gamertag: {gamertag} не существует");

            bool isPasswordValid = await userRepository.CheckPasswordAsync(userInfo.Id, password);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Неверный пароль");

            IList<string>? roles = await userRepository.GetRolesAsync(userInfo.Id);

            return tokenService.GenerateToken(userInfo.Id, userInfo.Email, roles);
        }

        public async Task<TokenDTO> RefreshTokenAsync(TokenDTO dto)
        {
            (bool isValid, string gamertag) = tokenService.ValidateToken(dto.AccessToken);

            if (!isValid)
                throw new UnauthorizedAccessException("Неверноеый Access token");

            UserInfo user = await userRepository.FindByGamertagAsync(gamertag);
            if (user == null)
                throw new UnauthorizedAccessException("Пользователь не найден");

            IList<string>? roles = await userRepository.GetRolesAsync(user.Id);

            return tokenService.GenerateToken(user.Id, user.Email, roles);
        }
    }
}