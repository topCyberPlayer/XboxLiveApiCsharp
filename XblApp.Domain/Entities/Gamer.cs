namespace XblApp.Domain.Entities
{
    /// <summary>
    /// Все аннотации описаны в классе GamerConfiguration через Fluent API
    /// </summary>
    public class Gamer
    {
        //todo GamerId должен стать Unique и заполняться БД, а в Xuid записывать, то что сейчас записывается в GamerId
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        
        /// <summary>
        /// Внешний ключ к IdentityUser
        /// </summary>
        public string? ApplicationUserId { get; set; }

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
