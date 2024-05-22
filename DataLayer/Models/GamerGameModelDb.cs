using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("GamerGame")]
    public class GamerGameModelDb
    {
        public long GamerId { get; set; }

        public long GameId { get; set; }

        public GamerModelDb Gamer { get; set; }

        public GameModelDb Game { get; set; }

        public int CurrentAchievements { get; set; }

        public int CurrentGamerscore { get; set; }
    }
}
