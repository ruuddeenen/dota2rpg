using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class HeroMemoryContext : IHeroContext
    {
        public Hero GetHeroById(string id)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetInventoryByHeroId(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Hero hero)
        {
            throw new NotImplementedException();
        }

        public bool UpdateExpierence(Hero hero, int exp)
        {
            throw new NotImplementedException();
        }
    }
}
