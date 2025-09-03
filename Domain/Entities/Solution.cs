namespace Domain.Entities
{
    /// <summary>
    /// Помощь для получения достижения
    /// </summary>
    public class Solution
    {
        public required long SolutionId { get; set; }
        /// <summary>
        /// Описание решения
        /// </summary>
        public required string Description { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ChangedAt { get; set; }

        //Связь с достижением
        //Если убрать AchievementId, то EF Core создаст теневое свойство (shadow property) в БД с таким же названием.
        //То есть физически колонка всё равно появится в таблице, просто в коде её не будет видно.
        //Это хуже для читаемости и контроля.
        public required long AchievementId { get; set; }
        //Навигационное свойство, нужно не для базы, а для удобной работы в C# коде.
        public required Achievement AchievementLink { get; set; }

        //Связь с игроком (автор решения)
        public long GamerId { get; set; }
        public required Gamer GamerLink { get; set; }
    }
}
