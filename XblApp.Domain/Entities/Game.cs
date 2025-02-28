namespace XblApp.Domain.Entities
{
    public class Game
    {
        public long GameId { get; set; }
        public string? GameName { get; set; }
        public int TotalAchievements { get; set; }
        public int TotalGamerscore { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? Description { get; set; }

        public ICollection<Achievement> AchievementLinks { get; set; } = []; // Коллекция достижений для этой игры
        public ICollection<GamerGame> GamerGameLinks { get; set; } = []; // Игроки, играющие в эту игру (связь через промежуточную таблицу)
    }
}
