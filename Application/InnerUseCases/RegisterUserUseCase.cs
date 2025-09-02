using Domain;
using Domain.DTO;
using Domain.Entities.JsonModels;
using Domain.Interfaces.Repository;
using Domain.Interfaces.XboxLiveService;

namespace Application.InnerUseCases
{
    /// <summary>
    /// Создает запись в таблицах: aspnetUsers и Gamer. Вызывается из UI и API
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="xblGamerService"></param>
    /// <param name="gamerRepository"></param>
    public class RegisterUserUseCase(
        IUserRepository userRepository,
        IXboxLiveGamerService xblGamerService,
        IGamerRepository gamerRepository)
    {
        public async Task<RegisterUserResult> RegisterUser(string gamertag, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(gamertag) || string.IsNullOrWhiteSpace(email))
                return new RegisterUserResult(success: false, error: "Gamertag and Email are required", null );

            UserInfo userInfo = await userRepository.FindByGamertagAsync(gamertag);
            if (userInfo is not null)
                return new RegisterUserResult(success: false, error : "This Gamertag is already linked", null );

            GamerJson resultGamer = await xblGamerService.GetGamerProfileAsync(gamertag);
            if (!resultGamer.ProfileUsers.Any())
                return new RegisterUserResult (success: false, error: "Gamer not found on Xbox Live", null);

            var createResult = await userRepository.CreateUserAsync(resultGamer.ProfileUsers.FirstOrDefault().Gamertag, email, password);
            if (!createResult.Success)
                return new RegisterUserResult (success: false, error: createResult.Error, null );

            var roleResult = await userRepository.AddRoleToUserAsync(createResult.UserId);

            await gamerRepository.SaveOrUpdateGamersAsync(resultGamer, createResult.UserId);

            return new RegisterUserResult (success: true,  error: null, userId: createResult.UserId);
        }
    }
}
