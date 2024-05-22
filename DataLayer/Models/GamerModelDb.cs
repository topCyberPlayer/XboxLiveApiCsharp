using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Gamers")]
    public class GamerModelDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GamerId { get; set; }
        
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(12)]
        public string? Gamertag { get; set; }

        [Required]
        public int Gamerscore { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(70)]
        public string? Bio { get; set; }

        public string? Location { get; set; }

        public ICollection<GamerGameModelDb> GameLinks { get; set; }// = new List<GamerGameModelDb>();
    }
}
