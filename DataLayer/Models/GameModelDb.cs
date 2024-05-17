using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class GameModelDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameId { get; set; }

        [Required]
        public string GameName { get; set; }

        [Required]
        public int TotalAchievements { get; set; }

        [Required]
        public int TotalGamerscore { get; set;}
    }
}
