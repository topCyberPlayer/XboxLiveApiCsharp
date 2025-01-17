using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IGamerRepository
    {
        public Task<Gamer> GetGamerProfileAsync(long id);
        public Task<Gamer> GetGamerProfileAsync(string gamertag);
        public Task<List<Gamer>> GetAllGamerProfilesAsync();
        public Task<Gamer> GetGamesForGamerAsync(string gamertag);
        public Task SaveGamerAsync(List<Gamer> gamers);
    }
}
