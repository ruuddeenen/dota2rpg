using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class AgilityHero : Hero
    {
        public AgilityHero(string name, float strGain, float agiGain, float intGain) : base(name, strGain, agiGain, intGain) { }

        public AgilityHero(string id, string name, int exp, float strGain, float agiGain, float intGain) : base(id, name, exp, strGain, agiGain, intGain) { }

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
