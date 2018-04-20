using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Models.AccountViewModel;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Logic;
using Microsoft.AspNetCore.Session;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserRepository _ApplicationUserRepo;

        public AccountController()
        {
            _ApplicationUserRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser(model.Username, model.Password, model.Email, model.FirstName, model.LastName);
                if (_ApplicationUserRepo.Insert(user))
                {
                    return RedirectToAction("Details", "Account");
                }

            }
            return View();
        }


        public ActionResult Edit(AccountEditViewModel model)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(AccountChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = HttpContext.Session.GetString("currentUserId");
                if (SecurePasswordHasher.Verify(model.CurrentPassword, _ApplicationUserRepo.GetUserById(currentUserId).Password))
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        Id = currentUserId,
                        Password = model.NewPassword
                    };
                    if (_ApplicationUserRepo.ChangePassword(user))
                    {
                        return RedirectToAction("Details", "Account");
                    }
                }
            }
            return View();
        }

    }
}