using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.XboxLiveServices
{
    internal class GameService : BaseService, IXboxLiveGameService
    {
        public GameService(IHttpClientFactory factory) : base(factory) { }
    }
}
