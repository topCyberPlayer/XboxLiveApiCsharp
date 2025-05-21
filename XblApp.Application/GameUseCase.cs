using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;

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
        public async Task<List<Game>> GetAllGamesAsync() => await _gameRepository.GetAllGamesAndGamerGameAsync();
        
        public async Task<Game> GetGameAsync(long gameId) => await _gameRepository.GetGameAndGamerGameAsync(gameId);

    }
}
