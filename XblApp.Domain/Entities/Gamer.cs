namespace XblApp.Domain.Entities
{
    /// <summary>
    /// Все аннотации описаны в классе GamerConfiguration через Fluent API
    /// </summary>
    public class Gamer
    {
        public long GamerId { get; set; } //ID из Xbox Live API
        public string? Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }

        public string? ApplicationUserId { get; set; } // Внешний ключ к IdentityUser

        public ICollection<GamerGame> GamerGameLinks { get; set; } = []; // Игры, в которые играет пользователь
        public ICollection<GamerAchievement> GamerAchievementLinks { get; set; } = []; // Связь с достижениями через промежуточную таблицу
    }
}
