using XblApp.Domain.Interfaces;

namespace XblApp.Application.InnerUseCases
{
    public class UserUseCase(IUserService userService)
    {
        public async Task Login(string gamertag, string password) =>
            await userService.LoginUserAsync(gamertag, password);
        

        public async Task RegisterUser(string gamertag, string email, string password) => 
            await userService.CreateUserAsync(gamertag, email, password);
    }
}
