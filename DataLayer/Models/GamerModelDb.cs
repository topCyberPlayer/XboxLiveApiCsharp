using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class GamerModelDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GamerId { get; set; }
        
        [Required]
        public string Gamertag { get; set; }

        [Required]
        public int Gamerscore { get; set; }

        public string? Bio { get; set; }

        public string? Location { get; set; }

        public ICollection<GamerGameModelDb> GameLinks { get; set; }
    }
}
