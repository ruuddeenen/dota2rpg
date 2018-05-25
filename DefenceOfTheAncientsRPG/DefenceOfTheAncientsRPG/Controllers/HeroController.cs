using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Models.HeroViewModel;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class HeroController : Controller
    {
        HeroRepository _heroRepo;
        ApplicationUserRepository _userRepo;

        public HeroController()
        {
            _heroRepo = new HeroRepository(new HeroSQLContext());
            _userRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            HeroCreateViewModel model = new HeroCreateViewModel()
            {
                Attributes = new List<Attribute> { Attribute.Strength, Attribute.Agility, Attribute.Intelligence }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(HeroCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Hero hero;
                if (model.MainAttribute == Attribute.Strength)
                {

                    hero = new StrengthHero
                        (
                        model.Name,
                        model.StrengthGain,
                        model.AgilityGain,
                        model.IntelligenceGain
                        );
                }
                else if (model.MainAttribute == Attribute.Agility)
                {
                    hero = new AgilityHero
                        (
                        model.Name,
                        model.StrengthGain,
                        model.AgilityGain,
                        model.IntelligenceGain
                        );
                }
                else
                {
                    hero = new IntelligenceHero
                        (
                        model.Name,
                        model.StrengthGain,
                        model.AgilityGain,
                        model.IntelligenceGain
                        );
                }
                _heroRepo.Insert(hero, _userRepo.GetUserById(HttpContext.Session.GetString("currentUserId")));
                return RedirectToAction("Select");
            }
            return View();
        }

        public IActionResult Select()
        {
            HeroSelectViewModel model = new HeroSelectViewModel(HttpContext.Session.GetString("currentUserId"));
            return View(model);
        }

        [HttpPost]
        public IActionResult Select(string id)
        {
            HttpContext.Session.SetString("currentHeroId", id);
            return RedirectToAction("Index", "Game", id);
        }
    }
}