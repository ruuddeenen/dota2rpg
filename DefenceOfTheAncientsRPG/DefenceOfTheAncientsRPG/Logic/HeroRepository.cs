using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Logic
{
    public class HeroRepository
    {
        private IHeroContext context;

        public HeroRepository(IHeroContext context)
        {
            this.context = context;
        }

        public Hero GetHeroById(string id)
        {
            return context.GetHeroById(id);
        }

        public List<Item> GetInventoryByHeroId(string id)
        {
            return context.GetInventoryByHeroId(id);
        }

        public bool Insert(Hero hero)
        {
            return context.Insert(hero);
        }

        public bool AddExpierence(Hero hero, int exp)
        {
            int setExp = exp + hero.Expierence;
            return context.UpdateExpierence(hero, setExp);
        }
    }
}
