using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XblApp.Domain.Entities
{
    public class Gamer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GamerId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Gamertag { get; set; }

        [Required]
        public int Gamerscore { get; set; }

        public string? Bio { get; set; }

        public string? Location { get; set; }

        // Игры, в которые играет пользователь
        public ICollection<GamerGame> GameLinks { get; set; } = new List<GamerGame>();

        // Связь с достижениями через промежуточную таблицу
        public ICollection<GamerAchievement> AchievementLinks { get; set; } = new List<GamerAchievement>();
    }
}
