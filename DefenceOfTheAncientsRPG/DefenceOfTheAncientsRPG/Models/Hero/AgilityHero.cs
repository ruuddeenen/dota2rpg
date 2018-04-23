using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class AgilityHero : Hero
    {
        public int Damage
        {
            get
            {
                int value = Agility;
                Inventory.ForEach(x => value += x.Agility);
                return value;
            }
        }
    }
}
