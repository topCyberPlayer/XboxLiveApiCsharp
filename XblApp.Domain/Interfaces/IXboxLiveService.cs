using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveGamerService
    {
        public Task<Gamer> GetGamerProfileAsync(string gamertag);

        public Task<Gamer> GetGamerProfileAsync(long xuid);
    }

    public interface IXboxLiveGameService
    {

    }
}
