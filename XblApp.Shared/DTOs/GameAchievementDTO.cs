using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Shared.DTOs
{
    public class GameAchievementDTO
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public List<GameAchievementInnerDTO> Achievements { get; set; }
    }

    public class GameAchievementInnerDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Score { get;set; }
    }
}
