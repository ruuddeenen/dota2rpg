using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public abstract class Hero
    {
        private readonly int ExpPerLevel = 1000;
        private readonly int HealthPerStrength = 20;
        private readonly float HealthRegenPerStrength = 0.8f;
        private readonly int AttackspeedPerAgility = 1;
        private readonly float ArmorPerAgility = 0.7f;
        private readonly float ManaPerIntelligence = 12f;
        private readonly float ManaRegenPerIntelligence = 2f;
        internal readonly List<Item> Inventory;
       
        /// <summary>
        /// Used for creating a new Hero
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strGain"></param>
        /// <param name="agiGain"></param>
        /// <param name="intGain"></param>
        public Hero(string name, float strGain, float agiGain, float intGain)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Expierence = 0;
            StrengthGain = strGain;
            AgilityGain = agiGain;
            IntelligenceGain = intGain;
        }

        /// <summary>
        /// Used for creating a hero fetched from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="exp"></param>
        /// <param name="strGain"></param>
        /// <param name="agiGain"></param>
        /// <param name="intGain"></param>
        public Hero(string id, string name, int exp, float strGain, float agiGain, float intGain)
        {
            Id = id;
            Name = name;
            Expierence = exp;
            StrengthGain = strGain;
            AgilityGain = agiGain;
            IntelligenceGain = intGain;
        }
        public string Id { get;  private set; }
        public string Name { get; private set; }
        public int Level
        {
            get
            {
                return Expierence / ExpPerLevel;
            }
        }
        public int Expierence { get; private set; }
        public int Strength
        {
            get
            {
                float value = Level * StrengthGain;
                Inventory.ForEach(x => value += x.Strength);
                return Convert.ToInt32(value);
            }
        }
        public float StrengthGain { get; private set; }
        public int Agility
        {
            get
            {
                float value = Level * AgilityGain;
                Inventory.ForEach(x => value += x.Agility);
                return Convert.ToInt32(value);
            }
        }
        public float AgilityGain { get; private set; }
        public int Intelligence
        {
            get
            {
                float value = Level * IntelligenceGain;
                Inventory.ForEach(x => value += x.Intelligence);
                return Convert.ToInt32(value);
            }
        }
        public float IntelligenceGain { get; private set; }
        public int Health
        {
            get
            {
                int value = Strength * HealthPerStrength;
                Inventory.ForEach(x => value += x.Health);
                return value;
            }
        }
        public float HealthRegen
        {
            get
            {
                float value = Strength * HealthRegenPerStrength;
                Inventory.ForEach(x => value += x.HealthRegen);
                return value;
            }
        }
        public int Attackspeed
        {
            get
            {
                int value = Agility * AttackspeedPerAgility;
                Inventory.ForEach(x => value += x.Attackspeed);
                return value;
            }
        }
        public int Armor
        {
            get
            {
                float value = Agility * ArmorPerAgility;
                Inventory.ForEach(x => value += x.Attackspeed);
                return Convert.ToInt32(value);
            }
        }
        public int Mana
        {
            get
            {
                float value = Intelligence * ManaPerIntelligence;
                Inventory.ForEach(x => value += x.Mana);
                return Convert.ToInt32(value);
            }
        }
        public float ManaRegen
        {
            get
            {
                float value = Intelligence * ManaRegenPerIntelligence;
                Inventory.ForEach(x => value += x.ManaRegen);
                return value;
            }
        }

    }
}
