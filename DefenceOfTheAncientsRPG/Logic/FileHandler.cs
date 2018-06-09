using DefenceOfTheAncientsRPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using DefenceOfTheAncientsRPG.Exceptions;

namespace DefenceOfTheAncientsRPG.Logic
{
    public class FileHandler
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileHandler(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<Item> CreateItemsFromExcel(IFormFile file)
        {
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            string fullPath = Path.Combine(newPath, file.FileName);

            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
            if (sFileExtension != ".xlsx")
            {
                throw new FileFormatException("File is not in .xlsx format.");
            }

            return GetItemsFromExcel(fullPath, file);

        }

        private List<Item> GetItemsFromExcel(string path, IFormFile file)
        {
            List<Item> Items = new List<Item>();
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Position = 0;
                ISheet sheet = new XSSFWorkbook(stream).GetSheetAt(0);
                int currentRow = 1;
                int cellCount = 12;                     // Item properties
                string[] item = new string[12];
                for (; ; )
                {
                    IRow row = sheet.GetRow(currentRow);
                    if (row == null) break;
                    for (int i = 0; i < cellCount; i++)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell != null)
                        {
                            item[i] = cell.ToString();
                        }
                        else item[i] = "0";
                    }
                    try
                    {
                        Items.Add(CreateItemFromArray(item));
                    }
                    catch { break; }
                    currentRow++;
                }
            }
            File.Delete(path);
            return Items;
        }

        private Item CreateItemFromArray(string[] array)
        {
            foreach (string s in array)
            {
                s.Replace(",", ".");
            }
            Item item = new Item(array[0])
            {
                Strength = Convert.ToInt32(array[1]),
                Agility = Convert.ToInt32(array[2]),
                Intelligence = Convert.ToInt32(array[3]),
                Health = Convert.ToInt32(array[4]),
                HealthRegen = Convert.ToSingle(array[5]),
                Attackspeed = Convert.ToInt32(array[6]),
                Armor = Convert.ToInt32(array[7]),
                Mana = Convert.ToInt32(array[8]),
                ManaRegen = Convert.ToSingle(array[9]),
                Damage = Convert.ToInt32(array[10]),
                Cost = Convert.ToInt32(array[11])
            };
            return item;
        }
    }
}
