using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XblApp.Domain.Entities
{
    public class Achievement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AchievementId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string? Description { get; set; }

        [Required]
        public int Gamerscore { get; set; }

        [Required]
        public bool IsSecret { get; set; }

        [ForeignKey("Game")]
        public long GameId { get; set; } // Внешний ключ к игре

        // Навигационное свойство для связи с игрой
        public Game Game { get; set; }

        public ICollection<GamerAchievement> GamerLinks { get; set; }
    }

    public class Achievement2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AchievementId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public int Gamerscore { get; set; }

        [Required]
        public bool IsSecret { get; set; }

        // Связь с игрой (1:N)
        public long GameId { get; set; }
        public Game Game { get; set; }

        // Игроки, которые разблокировали это достижение
        public ICollection<GamerAchievement> GamerLinks { get; set; } = new List<GamerAchievement>();
    }
}
