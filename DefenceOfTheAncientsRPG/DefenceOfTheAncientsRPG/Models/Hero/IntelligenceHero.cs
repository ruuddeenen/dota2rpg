using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class IntelligenceHero : Hero
    {
        public IntelligenceHero(string name, float strGain, float agiGain, float intGain) : base(name, strGain, agiGain, intGain)
        {

        }
        public IntelligenceHero(string id, string name, int exp, float strGain, float agiGain, float intGain) : base(id, name, exp, strGain, agiGain, intGain)
        {
        }

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
