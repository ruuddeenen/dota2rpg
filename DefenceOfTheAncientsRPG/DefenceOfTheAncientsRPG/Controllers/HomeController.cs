﻿using System;
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
        private AdministratorRepository _AdministratorRepository;
        public HomeController()
        {
            _ApplicationUserRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());
            _AdministratorRepository = new AdministratorRepository(new AdministratorSQLContext());
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
            ViewBag.ErrorMessage = string.Empty;
            try
            {
                if (_ApplicationUserRepo.Login(model.Username, model.Password))
                {
                    HttpContext.Session.SetString("currentUserId", _ApplicationUserRepo.GetUserByUsername(model.Username).Id);
                    return RedirectToAction("Select", "Hero");
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
                BlockedUserInfo info = _ApplicationUserRepo.GetBlockedUserInfoByUsername(model.Username);
                ViewBag.BlockedMessage = info.Message;

                System.Text.StringBuilder errormessage = new System.Text.StringBuilder();

                errormessage.Append(
                    string.Format(
                        "User {0} is blocked by admin {1}",
                    _ApplicationUserRepo.GetUserById(info.UserId).Username,
                    _AdministratorRepository.GetFullNameByAdminId(info.AdminId)));

                if (info.Until != null)
                {
                    errormessage
                        .Append(" until ")
                        .Append(info.Until.ToShortDateString());
                }
                errormessage.Append('.');

                ViewBag.ErrorMessage = errormessage.ToString();

                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Something went wrong, and we don't know what. Please try again.";
            }
            return View();
        }
    }
}
