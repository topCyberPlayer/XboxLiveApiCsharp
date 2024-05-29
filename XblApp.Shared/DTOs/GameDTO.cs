using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Shared.DTOs
{
    public class GameDTO
    {
        //public string? Gamertag { get; set; }
        //public long GamerId { get; set; }

        //public IEnumerable<>
        public long GameId { get; set; }
        public string? GameName { get; set; }
        public int TotalAchievements { get; set; }
        public int TotalGamerscore { get; set; }
    }
}
