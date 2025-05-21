using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;

namespace XblApp.Application.XboxLiveUseCases
{
    public class GameUseCase 
    {
        private readonly IGameRepository _gameRepository;

        public GameUseCase(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        /// <summary>
        /// Отобразить все игры: Название, Достижения, Геймерскор, Кол-во игроков
        /// </summary>
        /// <returns></returns>
        public async Task<List<Game>> GetGamesAsync() => await _gameRepository.GetGamesAndGamerGameAsync();
        
        public async Task<Game> GetGameAsync(long gameId) => await _gameRepository.GetGameAndGamerGameAsync(gameId);

    }
}
