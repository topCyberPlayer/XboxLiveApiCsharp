namespace XblApp.Domain.Entities
{
    public class GamerGame
    {
        public long GamerId { get; set; }
        public Gamer Gamer { get; set; }
        
        public long GameId { get; set; }
        public Game Game { get; set; }

        public int CurrentAchievements { get; set; }

        public int CurrentGamerscore { get; set; }
    }
}
