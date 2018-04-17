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
                ApplicationUser user = new ApplicationUser(false, model.Username, model.Password, model.Email, model.FirstName, model.LastName);
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
        

        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}