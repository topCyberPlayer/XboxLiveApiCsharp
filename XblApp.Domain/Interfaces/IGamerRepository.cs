using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IGamerRepository
    {
        public Task<List<Gamer>> GetAllGamerProfilesAsync();
        public Gamer GetGamerProfile(long id);
        public Gamer GetGamerProfile(string gamertag);
        public void SaveGamer(Gamer gamer);
    }
}
