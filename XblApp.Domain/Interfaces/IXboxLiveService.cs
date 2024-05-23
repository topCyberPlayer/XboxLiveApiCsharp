using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveService
    {
        public Task<Gamer> GetGamerProfileAsync(string gamertag);
    }
}
