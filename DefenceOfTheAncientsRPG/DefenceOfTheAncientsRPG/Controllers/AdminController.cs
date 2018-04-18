using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models.AdminViewModel;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;
using Microsoft.AspNetCore.Http;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class AdminController : Controller
    {
        private AdministratorRepository _AdminRepo;
        private ApplicationUserRepository _UserRepo;

        public AdminController()
        {
            _AdminRepo = new AdministratorRepository(new AdministratorSQLContext());
            _UserRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUsers(AdminManageUsersViewModel model)
        {
            return View(model);
        }

        public IActionResult ManageAdmins(AdminManageAdminsViewModel model)
        {
            return View(model);
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminLoginViewModel model)
        {
            Administrator admin = _AdminRepo.GetAdminById(model.Username);
            if (SecurePasswordHasher.Verify(model.Password, admin.PasswordHash)) // Logged in
            {
                HttpContext.Session.SetString("currentUserId", admin.ID);
                return RedirectToAction("ManageUsers");
            }
            return View();
        }


    }
}