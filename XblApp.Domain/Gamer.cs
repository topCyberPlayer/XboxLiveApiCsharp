using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Gamer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GamerId { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Gamertag { get; set; }

        [Required)]
        public int Gamerscore { get; set; }

        public string? Bio { get; set; }

        public string? Location { get; set; }

        public ICollection<GamerGame> GameLinks { get; set; }
    }
}
