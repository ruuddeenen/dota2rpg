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
using DefenceOfTheAncientsRPG.Exceptions;

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
            ViewData["Message"] = "Your about page";

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(HomeLoginViewModel model)
        {
            ViewBag.ErrorMessage = "";
            try
            {
                if (_ApplicationUserRepo.Login(model.Username, model.Password))
                {
                    HttpContext.Session.SetString("currentUserId", _ApplicationUserRepo.GetUserByUsername(model.Username).Id);
                    return RedirectToAction("Details", "Account");
                }
            }
            catch (EntryDoesNotExistException)
            {
                ViewBag.ErrorMessage = "User does not exist.";
                return View();

            }
            catch (IncorrectPasswordException)
            {
                ViewBag.ErrorMessage = "Incorrect username/password combination";
                return View();
            }
            catch (UserIsBlockedException)
            {
                ViewBag.ErrorMessage = "User is blocked"; // TO DO
                return View();
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong, and we don't know what. Please try again.";
            }
            return View();
        }
    }
}
