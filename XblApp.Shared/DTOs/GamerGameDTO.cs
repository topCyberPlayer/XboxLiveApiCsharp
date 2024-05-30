using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Shared.DTOs
{
    public class GamerGameDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public ICollection<GameDTO> Games { get; set; }
    }
}
