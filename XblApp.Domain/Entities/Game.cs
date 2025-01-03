﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XblApp.Domain.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long GameId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string GameName { get; set; }

        [Required]
        public int TotalAchievements { get; set; }

        [Required]
        public int TotalGamerscore { get; set; }

        public ICollection<GamerGame> GamerLinks { get; set; }
    }
}
