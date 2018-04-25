using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public interface IHeroContext
    {
        bool Insert(Hero hero);

        Hero GetHeroById(string id);

        List<Item> GetInventoryByHeroId(string id);

        bool UpdateExpierence(Hero hero, int exp);

        List<Hero> GetAllHeroes();

        List<Hero> GetHeroesByUserId(string id);

        bool InsertLink(Hero hero, ApplicationUser user);
    }
}
