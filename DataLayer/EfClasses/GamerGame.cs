namespace DataLayer.EfClasses
{
    public class GamerGame
    {
        public string GamerId { get; set; }

        public string GameId { get; set; }

        public Gamer Gamer { get; set; }

        public Game Game { get; set; }

        public int CurrentAchievements { get; set; }

        public int CurrentGamerscore { get; set; }

    }
}
