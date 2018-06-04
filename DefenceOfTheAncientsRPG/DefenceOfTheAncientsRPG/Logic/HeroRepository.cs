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

        public bool Insert(Hero hero, ApplicationUser user)
        {
            if (context.Insert(hero))
            {
                return context.InsertLink(hero, user);
            }
            return false;
        }

        public bool AddExpierence(Hero hero, int exp)
        {
            int setExp = exp + hero.Expierence;
            return context.UpdateExpierence(hero, setExp);
        }

        public List<Hero> GetAllHeroes()
        {
            return context.GetAllHeroes();
        }

        public List<Hero> GetHeroesByUserId(string id)
        {
            List<Hero> heroes = context.GetHeroesByUserId(id);

            if (heroes.Count > 3)
            {
                throw new Exception();
            }
            return heroes;
        }
    }
}
