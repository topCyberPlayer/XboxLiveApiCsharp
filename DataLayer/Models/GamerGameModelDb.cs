namespace DataLayer.Models
{
    public class GamerGameModelDb
    {
        public int GamerId { get; set; }

        public int GameId { get; set; }

        public GamerModelDb Gamer { get; set; }

        public GameModelDb Game { get; set; }

        public int CurrentAchievements { get; set; }

        public int CurrentGamerscore { get; set; }
    }
}
