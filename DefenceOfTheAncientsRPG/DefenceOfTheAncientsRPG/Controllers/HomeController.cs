using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Models.HomeViewModel;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationUserRepository _ApplicationUserRepo;
        public HomeController()
        {
            _ApplicationUserRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(HomeLoginViewModel model)
        {
            ApplicationUser user = _ApplicationUserRepo.GetUserByUsername(model.Username);
            if (SecurePasswordHasher.Verify(model.Password, user.PasswordHash))
            {

            }
            return View();
        }
    }
}
