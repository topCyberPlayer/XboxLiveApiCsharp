using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IGameRepository
    {
        public Task<Game> GetGameAsync(string gameName);
        public Task<List<Game>> GetAllGamesAsync();

        /// <summary>
        /// Сохранение происходит в 2 таблицы: Game и GamerGame
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public Task SaveGameAsync(Game game);
    }
}
