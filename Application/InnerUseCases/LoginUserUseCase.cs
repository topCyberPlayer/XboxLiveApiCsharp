using XblApp.Domain.Interfaces.Repository;
using XblApp.Domain.Responses;

namespace Application.InnerUseCases
{
    public class LoginUserUseCase(IUserRepository userRepository)
    {
        public async Task<LoginUserResult> Handle(string gamertag, string password)
        {
            var user = await userRepository.FindByGamertagAsync(gamertag);
            if (user == null)
                return new LoginUserResult { Success = false, Error = "Invalid credentials" };

            var valid = await userRepository.CheckPasswordAsync(user, password);
            if (!valid)
                return new LoginUserResult { Success = false, Error = "Invalid credentials" };

            var roles = await userRepository.GetRolesAsync(user);

            return new LoginUserResult
            {
                Success = true,
                UserId = user.Id,
                Email = user.Email,
                Roles = roles
            };
        }
    }
}