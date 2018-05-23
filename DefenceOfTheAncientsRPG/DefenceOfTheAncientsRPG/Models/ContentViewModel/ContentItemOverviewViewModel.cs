using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models.ContentViewModel
{
    public class ContentItemOverviewViewModel
    {
        public List<Item> Items;

        public ContentItemOverviewViewModel()
        {
            Items = new List<Item>();
        }
    }
}
