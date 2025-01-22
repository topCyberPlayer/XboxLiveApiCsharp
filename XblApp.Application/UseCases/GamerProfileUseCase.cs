using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application.UseCases
{
    public class GamerProfileUseCase : BaseUseCase
    {
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;

        private readonly IXboxLiveGameService _gameService;
        private readonly IGameRepository _gameRepository;

        public GamerProfileUseCase(
            IAuthenticationService authService,
            IAuthenticationRepository authRepository,
            IXboxLiveGamerService gamerService,
            IGamerRepository gamerRepository,
            IXboxLiveGameService gameService,
            IGameRepository gameRepository) : base(authService, authRepository)
        {
            _gamerRepository = gamerRepository;
            _gamerService = gamerService;

            _gameService = gameService;
            _gameRepository = gameRepository;
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
            if (IsDateXstsTokenExperid())
            {
                //Если дата XstsToken истекла, то запрашиваю OAuthToken и обновляю его
                TokenOAuth experidTokenOAuth = await _authRepository.GetTokenOAuth();

                await RefreshTokens(experidTokenOAuth);
            }

            string authorizationHeaderValue = _authRepository.GetAuthorizationHeaderValue();

            List<Gamer> gamers = await _gamerService.GetGamerProfileAsync(gamerId, authorizationHeaderValue);
            await _gamerRepository.SaveGamerAsync(gamers);

            List<Game> games = await _gameService.GetGamesForGamerProfileAsync(gamerId, authorizationHeaderValue);
            await _gameRepository.SaveGameAsync(games);

            return gamers.FirstOrDefault();
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