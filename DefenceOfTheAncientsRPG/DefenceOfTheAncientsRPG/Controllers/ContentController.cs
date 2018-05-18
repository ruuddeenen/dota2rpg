using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Models.ContentViewModel;
using Microsoft.AspNetCore.Http;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace DefenceOfTheAncientsRPG.Controllers
{
    public class ContentController : Controller
    {
        private readonly IHostingEnvironment _hostingEnviroment;
        public ContentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnviroment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(ContentAddItemViewModel model)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddItemFromFile(IFormFile file)
        {
            if (file != null)
            {
                FileHandler fh = new FileHandler(_hostingEnviroment);
                try
                {
                    List<Item> Items = fh.CreateItemsFromExcel(file);
                }
                catch (FileFormatException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("AddItem");
                }
                return RedirectToAction("Details", "Account");
            }
            return View();
        }
    }
}