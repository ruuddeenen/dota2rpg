using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class StrengthHero : Hero
    {
        public StrengthHero(string name, float strGain, float agiGain, float intGain) : base(name, strGain, agiGain, intGain) { }

        public StrengthHero(string id, string name, int exp, float strGain, float agiGain, float intGain) : base(id, name, exp, strGain, agiGain, intGain) { }

        public override int Damage
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
