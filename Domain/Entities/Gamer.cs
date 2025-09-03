using Domain.Entities.JsonModels;

namespace Domain.Entities
{
    /// <summary>
    /// Все аннотации описаны в классе GamerConfiguration через Fluent API
    /// </summary>
    public class Gamer
    {
        public required long GamerId { get; set; }
        public required string Gamertag { get; set; }
        public required int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }

        public required string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUserLink { get; set; } = null!;

        /// <summary>
        /// Игры, в которые играет пользователь
        /// </summary>
        public ICollection<GamerGame> GamerGameLinks { get; set; } = [];

        /// <summary>
        /// Связь с достижениями через промежуточную таблицу
        /// </summary>
        public ICollection<GamerAchievement> GamerAchievementLinks { get; set; } = [];

        /// <summary>
        /// Связь с решениями. Игрок может предлагать несколько решений для получения достижения
        /// </summary>
        public ICollection<Solution> SolutionLinks { get; set; } = [];
    }
}
