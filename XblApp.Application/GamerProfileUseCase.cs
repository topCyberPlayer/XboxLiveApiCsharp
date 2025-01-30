using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application
{
    public class GamerProfileUseCase : BaseUseCase
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

        public async Task<Gamer> GetGamerProfileAsync(string gamertag)
        {
            Gamer gamer = await _gamerRepository.GetGamerProfileAsync(gamertag);

            return gamer;
        }

        public async Task<List<Gamer>> GetAllGamerProfilesAsync()
        {
            List<Gamer> gamers = await _gamerRepository.GetAllGamerProfilesAsync();

            return gamers;
        }

        public async Task<Gamer> GetGamesForGamerAsync(string gamertag)
        {
            Gamer gamesForGamer = await _gamerRepository.GetGamesForGamerAsync(gamertag);

            return gamesForGamer;
        }

        public async Task<Gamer?> UpdateProfileAsync(long gamerId)
        {
            //Если дата XstsToken истекла, то запрашиваю OAuthToken и обновляю его
            if (IsDateXstsTokenExperid())
            {
                TokenOAuth experidTokenOAuth = await _authRepository.GetTokenOAuth();

                await RefreshTokens(experidTokenOAuth);
            }

            List<Gamer> gamers = await _gamerService.GetGamerProfileAsync(gamerId);
            await _gamerRepository.SaveGamerAsync(gamers);

            List<Game> games = await _gameService.GetGamesForGamerProfileAsync(gamerId);
            await _gameRepository.SaveGameAsync(games);

            List<Achievement> achievements = await _achievementService.GetAchievementsXboxoneRecentProgressAndInfoAsync(gamerId);
            await _achievementRepository.SaveAchievementsAsync(achievements);
            
            return await _gamerRepository.GetGamerProfileAsync(gamerId);
        }

        private bool IsDateXstsTokenExperid()
        {
            DateTime? dateNow = DateTime.Now;

            DateTime? dateDb = _authRepository.GetDateXstsTokenExpired();

            return dateNow > dateDb ? true : false;
        }

        public bool IsDateXauTokenExperid()
        {
            DateTime? dateNow = DateTime.Now;

            DateTime? dateDb = _authRepository.GetDateXauTokenExpired();

            return dateNow > dateDb ? true : false;
        }
    }
}