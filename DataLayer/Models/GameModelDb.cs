using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Games")]
    public class GameModelDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GameId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? GameName { get; set; }

        [Required]
        public int TotalAchievements { get; set; }

        [Required]
        public int TotalGamerscore { get; set;}
    }
}
