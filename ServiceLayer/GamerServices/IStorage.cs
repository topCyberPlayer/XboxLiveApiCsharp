using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Logic
{
    public interface IStorage
    {
        public GamerModelDto FindByGamertag(string gamertag);
        public IEnumerable<GamerModelDto> GetAllGamers();
    }
}
