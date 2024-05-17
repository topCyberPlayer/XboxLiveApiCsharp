using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class GamerModelDto
    {
        public long GamerId { get; set; }

        public string Gamertag { get; set; }

        public int Gamerscore { get; set; }

        public int GamesCount { get; set; }
    }
}
