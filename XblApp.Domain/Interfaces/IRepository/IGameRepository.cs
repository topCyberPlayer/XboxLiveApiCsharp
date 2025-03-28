using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IGameRepository
    {
        public Task<Game> GetGameAndGamerGameAsync(string gameName);
        public Task<List<Game>> GetAllGamesAndGamerGameAsync();
        /// <summary>
        /// Сохранение происходит в 2 таблицы: Game и GamerGame
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public Task SaveOrUpdateGamesAsync(GameJson games);
    }
}
