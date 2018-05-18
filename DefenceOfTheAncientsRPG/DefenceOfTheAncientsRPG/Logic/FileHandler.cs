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
            string sFileExtension = Path.GetExtension(file.FileName).ToLower();
            if (sFileExtension != ".xlsx")
            {
                throw new FileFormatException("File is not in .xlsx format.");
            }
            ISheet sheet;
            string fullPath = Path.Combine(newPath, file.FileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Position = 0;
                XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                sheet = hssfwb.GetSheetAt(0);
                List<Item> Items = new List<Item>();
                int currentRow = 1;
                IRow row;
                int cellCount = 12;                     // Item properties
                string[] item = new string[12];
                for (; ; )
                {
                    row = sheet.GetRow(currentRow);
                    if (row == null) break;
                    for (int i = 0; i < cellCount; i++)
                    {
                        ICell cell = row.GetCell(i);
                        item[i] = cell.ToString();
                    }
                    Items.Add(CreateItemFromArray(item));
                    currentRow++;
                }
                return Items;
            }
        }

        private Item CreateItemFromArray(string[] array)
        {
            foreach (string s in array)
            {
                s.Replace(",", ".");
            }
            return new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = array[0],
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
        }
    }
}
