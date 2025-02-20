namespace XblApp.Domain.Entities
{
    public class GamerGame
    {
        public long GamerId { get; set; }
        public Gamer GamerLink { get; set; } = null!; // Навигационное свойство

        public long GameId { get; set; }
        public Game GameLink { get; set; } = null!; // Навигационное свойство

        public int CurrentAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
    }

}
