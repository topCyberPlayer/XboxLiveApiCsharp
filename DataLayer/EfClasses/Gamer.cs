using System.ComponentModel.DataAnnotations;

namespace DataLayer.EfClasses
{
    public class Gamer
    {
        [Key]
        public int GamerId { get; set; }
        [Required]
        public string Gamertag { get; set; }
        public string? AccountTier { get; set; }
        public string? Bio { get; set; }


        //relationships
        public ICollection<GamerGame> GameLinks { get; set; }
    }
}
