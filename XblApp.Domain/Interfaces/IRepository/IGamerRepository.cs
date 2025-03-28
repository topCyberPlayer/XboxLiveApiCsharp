using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IGamerRepository
    {
        public Task<Gamer> GetGamerProfileAsync(long id);
        public Task<Gamer> GetGamerProfileAsync(string gamertag);
        public Task<List<Gamer>> GetAllGamerProfilesAsync();
        public Task<Gamer> GetGamesForGamerAsync(string gamertag);
        public Task SaveOrUpdateGamersAsync(GamerJson gamers);
    }
}
