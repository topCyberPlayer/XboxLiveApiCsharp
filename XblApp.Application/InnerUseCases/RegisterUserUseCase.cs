using XblApp.Domain.Interfaces;

namespace XblApp.Application.InnerUseCases
{
    public class RegisterUserUseCase(IRegisterUserService registerUserService)
    {
        public async Task RegisterUser(string gamertag, string email, string password) => 
            await registerUserService.CreateUserAsync(gamertag, email, password);
    }
}
