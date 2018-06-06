using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Exceptions;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class HeroMemoryContext : IHeroContext
    {
        private List<Hero> Heroes;

        public HeroMemoryContext()
        {
            Heroes = new List<Hero>();
        }
        public List<Hero> GetAllHeroes()
        {
            return Heroes;
        }

        public Hero GetHeroById(string id)
        {
            foreach (Hero hero in Heroes)
            {
                if (hero.Id == id)
                {
                    return hero;
                }
            }
            throw new EntryDoesNotExistException(string.Format("User with id: {0} could not be found", id));
        }

        public List<Hero> GetHeroesByUserId(string id)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetInventoryByHeroId(string id)
        {
            throw new NotImplementedException();
        }

        public List<Attribute> GetUsedAttributes()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Hero hero)
        {
            try
            {
                Heroes.Add(hero);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool InsertLink(Hero hero, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateExpierence(Hero hero, int exp)
        {
            throw new NotImplementedException();
        }
    }
}
