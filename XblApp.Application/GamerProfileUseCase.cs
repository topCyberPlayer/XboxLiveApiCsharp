using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application
{
    public class GamerProfileUseCase : AuthenticationUseCase
    {
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;

        private readonly IXboxLiveGameService _gameService;
        private readonly IGameRepository _gameRepository;

        private readonly IXboxLiveAchievementService _achievementService;
        private readonly IAchievementRepository _achievementRepository;

        public GamerProfileUseCase(
            IXboxLiveAuthenticationService authService,
            IAuthenticationRepository authRepository,
            IXboxLiveGamerService gamerService,
            IGamerRepository gamerRepository,
            IXboxLiveGameService gameService,
            IGameRepository gameRepository,
            IXboxLiveAchievementService achievementService,
            IAchievementRepository achievementRepository) : base(authService, authRepository)
        {
            _gamerService = gamerService;
            _gamerRepository = gamerRepository;

            _gameService = gameService;
            _gameRepository = gameRepository;

            _achievementService = achievementService;
            _achievementRepository = achievementRepository;
        }

        public async Task<Gamer> GetGamerProfileAsync(string gamertag) =>
            await _gamerRepository.GetGamerProfileAsync(gamertag);

        public async Task<List<Gamer>> GetAllGamerProfilesAsync() =>
            await _gamerRepository.GetAllGamerProfilesAsync();

        public async Task<Gamer> GetGamesForGamerAsync(string gamertag) =>
            await _gamerRepository.GetGamesForGamerAsync(gamertag);


        public async Task<Gamer?> UpdateProfileAsync(long gamerId)
        {
            List<Gamer> gamers = await _gamerService.GetGamerProfileAsync(gamerId);
            await _gamerRepository.SaveOrUpdateGamersAsync(gamers);

            List<Game> games = await _gameService.GetGamesForGamerProfileAsync(gamerId);
            await _gameRepository.SaveOrUpdateGamesAsync(games);

            //List<Achievement> achievements = await _achievementService.GetAchievementsXboxoneRecentProgressAndInfoAsync(gamerId);
            //await _achievementRepository.SaveAchievementsAsync(achievements);
            
            return await _gamerRepository.GetGamerProfileAsync(gamerId);
        }
    }
}