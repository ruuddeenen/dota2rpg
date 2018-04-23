using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Health { get; set; }
        public int Attackspeed { get; set; }
        public int Armor { get; set; }
        public int Mana { get; set; }
    }
}
