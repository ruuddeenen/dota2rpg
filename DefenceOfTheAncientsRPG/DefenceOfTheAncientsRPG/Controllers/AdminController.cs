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
using System.Net.Mail;
using DefenceOfTheAncientsRPG.Exceptions;

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
                if (_AdminRepo.Insert(admin))
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
            Administrator admin = _AdminRepo.GetAdminByUsername(model.Username);
            ViewBag.ErrorMessage = string.Empty;
            try
            {
                if (admin.Activated)
                {
                    if (_AdminRepo.Login(model.Username, model.Password))
                    {
                        HttpContext.Session.SetString("currentUserId", admin.Id);
                        return RedirectToAction("ManageAdmins");
                    }
                }
                else
                {
                    if (model.Password == admin.Password)
                    {
                        HttpContext.Session.SetString("currentUserId", admin.Id);
                        return RedirectToAction("ChangePassword");
                    }
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
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong, and we don't know what. Please try again.";
            }
            return View();
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(AdminChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = HttpContext.Session.GetString("currentUserId");
                Administrator currentAdmin = _AdminRepo.GetAdminById(currentUserId);
                if (currentAdmin.Activated)
                {
                    ViewBag.Warning = "Change your password often to be safe";
                    if (!SecurePasswordHasher.Verify(model.CurrentPassword, currentAdmin.Password))
                        return View();
                }
                else ViewBag.Warning = "Please change your password to activate your administrator account!";
                {
                    if (model.CurrentPassword != currentAdmin.Password)
                        return View();
                }
                currentAdmin.Password = model.NewPassword;
                if (_AdminRepo.ChangePassword(currentUserId, model.NewPassword))
                {
                    return RedirectToAction("ManageAdmins");
                }

            }
            return View();
        }

        public IActionResult Block(string id)
        {
            AdminBlockViewModel model = new AdminBlockViewModel
            {
                AdminId = HttpContext.Session.GetString("currentUserId"),
                UserId = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Block(AdminBlockViewModel model)
        {
            if (ModelState.IsValid)
            {
                BlockedUserInfo info;
                if (model.Permanent)
                {
                    info = new BlockedUserInfo(model.Message, model.UserId, model.AdminId);
                }
                else
                {
                    info = new BlockedUserInfo(model.Message, model.UserId, model.AdminId, model.Until);
                }

                if (_UserRepo.BlockUser(info))
                {
                    return RedirectToAction("ManageUsers");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Unblock(string id)
        {   
            _UserRepo.UnblockUser(id);
            return RedirectToAction("ManageUsers", new AdminManageUsersViewModel());
        }
    }
}