using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Shared.DTOs
{
    public class GamerGameAchievementDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public List<GameAchievementDTO2> GameAchievements { get; set; }
    }

    public class GameAchievementDTO2
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public List<GamerAchievementInnerDTO> Achievements { get; set; }
    }

    public class GamerAchievementInnerDTO : GameAchievementInnerDTO
    {
        public bool IsUnlocked { get; set; }
    }
}
