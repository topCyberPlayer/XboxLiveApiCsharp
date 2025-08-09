using Microsoft.AspNetCore.Identity;
using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Infrastructure.Models;

namespace XblApp.InternalService
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;

        public UserService(
            UserManager<ApplicationUser> userManager, 
            IXboxLiveGamerService gamerService,
            IGamerRepository gamerRepository)
        {
            _userManager = userManager;
            _gamerService = gamerService;
            _gamerRepository = gamerRepository;
        }

        public async Task<(bool Success, string UserId, IEnumerable<string> Errors)> CreateUserAsync(string gamertag, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(gamertag) || string.IsNullOrWhiteSpace(email))
                return (false, "", new[] { "Gamertag and Email are required" });

            if (await _gamerRepository.IsGamertagLinkedToUserAsync(gamertag))
                return (false, "", new[] { "This Gamertag is already linked" });

            GamerJson resultGamer = await _gamerService.GetGamerProfileAsync(gamertag);
            if(!resultGamer.ProfileUsers.Any())
                return (false, "", new[] { "Gamer not found on Xbox Live" });

            ApplicationUser user = new() { UserName = gamertag, Email = email, CreatedAt = DateTime.UtcNow };

            IdentityResult resultIdentity = await _userManager.CreateAsync(user, password);

            if (!resultIdentity.Succeeded)
                return (false, "", resultIdentity.Errors.Select(e => e.Description));

            resultGamer.ProfileUsers.First().ApplicationUserId = user.Id;
            await _gamerRepository.SaveOrUpdateGamersAsync(resultGamer);
            await _userManager.AddToRoleAsync(user, "gamerTeam");

            return (true, user.Id, []);
        }

        public Task<string> LoginUserAsync(string gamertag, string password)
        {
            throw new NotImplementedException();
        }
    }
}
