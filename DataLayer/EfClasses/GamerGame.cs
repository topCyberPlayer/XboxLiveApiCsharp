using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class GamerGame
    {
        public string GamerId { get; set; }
        public string GameId { get; set; }
        public Gamer Gamer { get; set; }
        public Game Game { get; set; }

    }
}
