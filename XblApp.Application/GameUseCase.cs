using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Application
{
    public class GameUseCase : AuthenticationUseCase
    {
        private readonly IGameRepository _gameRepository;

        public GameUseCase(
            IGameRepository gameRepository,
            IXboxLiveAuthenticationService authService,
            IAuthenticationRepository authRepository) : base(authService, authRepository)
        {
            _gameRepository = gameRepository;
        }
        /// <summary>
        /// Отобразить все игры: Название, Достижения, Геймерскор, Кол-во игроков
        /// </summary>
        /// <returns></returns>
        public async Task<List<Game>> GetAllGamesAsync()
        {
            List<Game> games = await _gameRepository.GetAllGamesAndGamerGameAsync();

            return games;
        }
    }
}
