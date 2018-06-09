using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;


namespace DefenceOfTheAncientsRPG.Models.HeroViewModel
{
    public class HeroSelectViewModel
    {
        private HeroRepository _heroRepo;

        public List<Hero> userHeroes;

        public HeroSelectViewModel(string id)
        {
            _heroRepo = new HeroRepository(new HeroSQLContext());

            userHeroes = _heroRepo.GetHeroesByUserId(id);
        }
    }
}
