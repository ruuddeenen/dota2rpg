using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;

namespace DefenceOfTheAncientsRPG.Models
{
    public class Hero
    {
        private HeroRepository _heroRepo = new HeroRepository(new HeroSQLContext());
        private readonly int ExpPerLevel = 1000;
        private readonly int HealthPerStrength = 20;
        private readonly float HealthRegenPerStrength = 0.8f;
        private readonly int AttackspeedPerAgility = 1;
        private readonly float ArmorPerAgility = 0.7f;
        private readonly float ManaPerIntelligence = 12;
        private readonly float ManaRegenPerIntelligence = 2;
        internal readonly List<Item> Inventory;

        public Hero()
        {
            Inventory = new List<Item>(); // Get Inventory from repo
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Expierence { get; private set; }
        public int Strength { get; private set; }
        public float StrengthGain { get; private set; }
        public int Agility { get; private set; }
        public float AgilityGain { get; private set; }
        public int Intelligence { get; private set; }
        public float IntelligenceGain { get; private set; }
        public int Health { get; private set; }
        public float HealthRegen { get; private set; }
        public int Attackspeed { get; private set; }
        public int Armor { get; private set; }
        public int Mana { get; private set; }
        public float ManaRegen { get; private set; }

    }
}
