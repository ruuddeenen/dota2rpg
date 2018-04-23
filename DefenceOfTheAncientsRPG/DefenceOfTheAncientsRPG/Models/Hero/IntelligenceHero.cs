using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class IntelligenceHero : Hero
    {
        public int Damage
        {
            get
            {
                int value = Intelligence;
                Inventory.ForEach(x => value += x.Intelligence);
                return value;
            }
        }
    }
}
