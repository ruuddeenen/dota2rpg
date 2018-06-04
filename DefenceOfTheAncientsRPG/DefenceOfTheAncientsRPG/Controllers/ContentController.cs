using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Models.ContentViewModel;
using Microsoft.AspNetCore.Http;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Exceptions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

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

        public IActionResult Items()
        {
            return View(new ContentItemOverviewViewModel
            {
                Items = _itemRepo.GetAllItems()
            });
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
                return View("ItemOverview");
            }
            else
            {
                FileHandler fh = new FileHandler(_hostingEnviroment);
                try
                {
                    List<Item> Items = fh.CreateItemsFromExcel(collection.Files[0]);
                    foreach (Item item in Items)
                    {
                        _itemRepo.Insert(item);
                    }
                    return View("ItemOverview", new ContentItemOverviewViewModel { Items = _itemRepo.GetAllItems() });
                }
                catch (FileFormatException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("ItemOverview");
                }
            }
        }

        [NonAction]
        private Item CreateItemFromCollection(IFormCollection collection, int index)
        {
            Item item = new Item(collection["Name"][index]);
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

        public IActionResult DownloadItemTemplate()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\addContentTemplates\");
            string fileName = "item-template.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public IActionResult ItemOverview()
        {
            ContentItemOverviewViewModel model = new ContentItemOverviewViewModel
            {
                Items = _itemRepo.GetAllItems().OrderBy(x => x.Cost).ToList()
            };
            return View(model);
        }
    }
}