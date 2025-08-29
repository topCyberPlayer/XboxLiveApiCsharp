namespace Domain.Entities
{
    /// <summary>
    /// Все аннотации описаны в классе GamerConfiguration через Fluent API
    /// </summary>
    public class Gamer
    {
        public required long GamerId { get; set; }
        public required string Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Игры, в которые играет пользователь
        /// </summary>
        public ICollection<GamerGame> GamerGameLinks { get; set; } = [];

        /// <summary>
        /// Связь с достижениями через промежуточную таблицу
        /// </summary>
        public ICollection<GamerAchievement> GamerAchievementLinks { get; set; } = [];
    }
}
