using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Domain.Entities
{
    public class GamerAchievement
    {
        public long GamerId { get; set; }
        public Gamer GamerLink { get; set; }

        public long AchievementId { get; set; }
        public Achievement AchievementLink { get; set; }

        public DateTime DateUnlocked { get; set; }
    }
}
