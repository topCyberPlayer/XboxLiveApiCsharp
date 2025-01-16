using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IGameRepository
    {
        public Task<Game> GetGameAsync(string gameName);
        public Task<List<Game>> GetAllGamesAsync();
        public Task SaveGamesAsync(GamerGame game);
    }
}
