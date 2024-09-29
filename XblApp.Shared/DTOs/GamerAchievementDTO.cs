using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Shared.DTOs
{
    public class GamerAchievementDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
    }
}
