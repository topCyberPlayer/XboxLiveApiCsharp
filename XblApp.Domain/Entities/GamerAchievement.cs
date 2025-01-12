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
        public Gamer Gamer { get; set; }

        public long AchievementId { get; set; }
        public Achievement Achievement { get; set; }

        public DateTime DateUnlocked { get; set; }
    }
}
