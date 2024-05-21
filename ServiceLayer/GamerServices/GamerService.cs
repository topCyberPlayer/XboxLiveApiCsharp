using DataLayer.Logic;
using ServiceLayer.Models;

namespace ServiceLayer.GamerServices
{
    public class GamerService
    {
        private IStorage _storage;

        public GamerService(IStorage storage)
        {
            _storage = storage;
        }

        public GamerModelDto FindByGamertag(string gamertag)
        {
            GamerModelDto gamer = _storage.FindByGamertag(gamertag);

            return gamer;
        }

        public IEnumerable<GamerModelDto> GetAllGamers()
        {
            IEnumerable<GamerModelDto> allGamers = _storage.GetAllGamers();

            return allGamers;
        }
    }
}
