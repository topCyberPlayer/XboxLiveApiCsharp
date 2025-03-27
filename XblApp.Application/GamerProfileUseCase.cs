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

        public async Task<Gamer> GetGamerProfileRepoAsync(string gamertag) =>
            await _gamerRepository.GetGamerProfileAsync(gamertag);

        public async Task<List<Gamer>> GetAllGamerProfilesRepoAsync() =>
            await _gamerRepository.GetAllGamerProfilesAsync();

        public async Task<Gamer> GetGamesForGamerRepoAsync(string gamertag) =>
            await _gamerRepository.GetGamesForGamerAsync(gamertag);


        public async Task<Gamer?> UpdateProfileAsync(long gamerId)
        {
            List<Gamer> gamers = await GetAndSaveGamerProfile(gamerId);

            List<Game> games = await GetAndSaveGames(gamerId);

            await GetAchievementsParallels(gamerId, games);

            return gamers.First();
        }

        public async Task<List<Gamer>> GetAndSaveGamerProfile(long gamerId)
        {
            List<Gamer> gamers = await _gamerService.GetGamerProfileAsync(gamerId);
            await _gamerRepository.SaveOrUpdateGamersAsync(gamers);

            return gamers;
        }

        public async Task<List<Game>> GetAndSaveGames(long gamerId)
        {
            List<Game> games = await _gameService.GetGamesForGamerProfileAsync(gamerId);
            await _gameRepository.SaveOrUpdateGamesAsync(games);

            return games;
        }

        public async Task GetAchievementsParallels(long gamerId, List<Game> games)
        {
            var semaphore = new SemaphoreSlim(5); // Ограничиваем до 5 параллельных задач

            await Task.WhenAll(games.Select(async game =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await GetAndSaveAchievements(gamerId, game.GameId);
                }
                finally
                {
                    semaphore.Release();
                }
            }));

        }

        public async Task GetAndSaveAchievements(long gamerId, long gameId)
        {
            (List<Achievement> achievements, List<GamerAchievement> gamerAchievements) = await _achievementService.GetAchievementsAsync(gamerId, gameId);
            await _achievementRepository.SaveOrUpdateAchievementsAsync(achievements);
            //await _achievementRepository.SaveOrUpdateGamerAchievementsAsync(gamerAchievements);
        }
    }
}