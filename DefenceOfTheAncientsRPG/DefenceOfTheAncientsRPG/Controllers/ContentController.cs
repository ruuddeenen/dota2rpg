﻿using System;
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
        private ItemRepository _itemRepo;
        public ContentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnviroment = hostingEnvironment;
            _itemRepo = new ItemRepository(new Data.ItemSQLContext());
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
        public IActionResult AddItem(IFormCollection collection)
        {
            if (collection.Files == null)
            {
                List<Item> Items = new List<Item>();

                for (int i = 0; i < collection["Name"].Count; i++)
                {
                    Items.Add(CreateItemFromCollection(collection, i));
                }

                foreach (Item item in Items)
                {
                    _itemRepo.Insert(item);
                }
                return View();
            }
            else
            {
                FileHandler fh = new FileHandler(_hostingEnviroment);
                try
                {
                    List<Item> Items = fh.CreateItemsFromExcel(collection.Files[0]);
                }
                catch (FileFormatException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("AddItem");
                }
                return RedirectToAction("Details", "Account");
            }
        }

        [NonAction]
        private Item CreateItemFromCollection(IFormCollection collection, int index)
        {
            Item item = new Item
            {
                Name = collection["Name"][index]
            };
            int.TryParse(collection["Strength"][index], out int res);
            item.Strength = res;
            int.TryParse(collection["Agility"][index], out res);
            item.Agility = res;
            int.TryParse(collection["Intelligence"][index], out res);
            item.Intelligence = res;
            int.TryParse(collection["Health"][index], out res);
            item.Health = res;
            float.TryParse(collection["HealthRegen"][index], out float resF);
            item.HealthRegen = resF;
            int.TryParse(collection["Attackspeed"][index], out res);
            item.Attackspeed = res;
            int.TryParse(collection["Armor"][index], out res);
            item.Armor = res;
            int.TryParse(collection["Mana"][index], out res);
            item.Mana = res;
            float.TryParse(collection["ManaRegen"][index], out resF);
            item.ManaRegen = resF;
            int.TryParse(collection["Damage"][index], out res);
            item.Damage = res;
            int.TryParse(collection["Cost"][index], out res);
            item.Cost = res;

            return item;
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