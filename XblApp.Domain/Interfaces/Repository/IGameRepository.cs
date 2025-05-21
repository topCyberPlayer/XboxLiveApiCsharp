using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IGameRepository
    {
        public Task<Game> GetGameAndGamerGameAsync(long gameId);
        
        public Task<List<Game>> GetGamesAndGamerGameAsync();
        
        /// <summary>
        /// Сохранение происходит в 2 таблицы: Game и GamerGame
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public Task SaveOrUpdateGamesAsync(GameJson games);
    }
}
