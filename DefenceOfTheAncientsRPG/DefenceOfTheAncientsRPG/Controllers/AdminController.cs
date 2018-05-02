﻿using System;
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
            if (admin.Activated)
            {
                if (SecurePasswordHasher.Verify(model.Password, admin.Password)) // Logged in
                {
                    HttpContext.Session.SetString("currentUserId", admin.ID);
                    return RedirectToAction("ManageAdmins");
                }
            }
            else
            {
                if (model.Password == admin.Password)
                {
                    HttpContext.Session.SetString("currentUserId", admin.ID);
                    return RedirectToAction("ChangePassword");
                }
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
                Administrator currentAdmin = _AdminRepo.GetAdminById(HttpContext.Session.GetString("currentUserId"));
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
                if (_AdminRepo.ChangePassword(currentAdmin))
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
                BlockedUserInfo info = new BlockedUserInfo
                {
                    UserId = model.UserId,
                    AdminId = model.AdminId,
                    Message = model.Message,
                    Since = model.From,
                    Until = model.Until
                };

                if (_UserRepo.BlockUser(info))
                {
                    return RedirectToAction("ManageUsers");
                }

            }
            return View();
        }
    }
}