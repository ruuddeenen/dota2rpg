using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class StrengthHero : Hero
    {
        public int Damage
        {
            get
            {
                int value = Strength;
                Inventory.ForEach(x => value += x.Strength);
                return value;
            }
        }
    }
}
