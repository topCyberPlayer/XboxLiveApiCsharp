using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.Interfaces.Repository;
using XblApp.Domain.Responses;

namespace Application.InnerUseCases
{
    public class RegisterUserUseCase(
        IUserRepository userRepository,
        IXboxLiveGamerService xblGamerService,
        IGamerRepository gamerRepository)
    {
        public async Task<RegisterUserResult> RegisterUser(string gamertag, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(gamertag) || string.IsNullOrWhiteSpace(email))
                return new RegisterUserResult { Success = false, Error = "Gamertag and Email are required" };

            if (await gamerRepository.IsGamertagLinkedToUserAsync(gamertag))
                return new RegisterUserResult { Success = false, Error = "This Gamertag is already linked" };

            GamerJson resultGamer = await xblGamerService.GetGamerProfileAsync(gamertag);
            if (!resultGamer.ProfileUsers.Any())
                return new RegisterUserResult { Success = false, Error = "Gamer not found on Xbox Live" };

            var result = await userRepository.CreateUserAsync(gamertag, email, password);
            if (!result.Success)
                return new RegisterUserResult { Success = false, Error = result.Error };

            await gamerRepository.SaveOrUpdateGamersAsync(resultGamer);

            return new RegisterUserResult { UserId = result.UserId, Success = true };
        }
    }
}
