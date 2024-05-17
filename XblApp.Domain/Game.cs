using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string GameName { get; set; }

        [Required]
        public int TotalAchievements { get; set; }

        [Required]
        public int TotalGamerscore { get; set;}
    }
}
