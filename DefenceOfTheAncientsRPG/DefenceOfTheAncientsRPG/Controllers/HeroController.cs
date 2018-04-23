using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Models.HeroViewModel;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class HeroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
                
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HeroCreateViewModel model)
        {
            return View();
        }
    }
}