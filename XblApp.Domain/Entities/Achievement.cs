using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XblApp.Domain.Entities
{
    public class Achievement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AchievementId { get; set; }

        [ForeignKey("Game")]
        public long GameId { get; set; } // Внешний ключ к игре

        [Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string? Description { get; set; }

        [Required]
        public int Gamerscore { get; set; }

        [Required]
        public bool IsSecret { get; set; }

        [Required]
        public DateTime DateUnlock {  get; set; }
        

        // Навигационное свойство для связи с игрой
        public Game Game { get; set; }

        public ICollection<GamerAchievement> GamerLinks { get; set; }
    }
}
