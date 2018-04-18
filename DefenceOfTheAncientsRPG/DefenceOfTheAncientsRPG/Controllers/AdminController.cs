using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models.AdminViewModel;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data.Admin;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.AspNetCore.Http;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class AdminController : Controller
    {
        private AdministratorRepository _AdminRepo;

        public AdminController()
        {
            _AdminRepo = new AdministratorRepository(new AdministratorSQLContext());

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult ManageAdmins()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Administrator admin = new Administrator(model.Password, model.FirstName, model.LastName);
                if(_AdminRepo.Insert(admin))
                {
                    return RedirectToAction("ManageAdmins");
                }
            }
            return View();
        }

    }
}