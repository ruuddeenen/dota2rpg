using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ItemMemoryContext : IItemContext
    {
        private List<Item> Items;

        public ItemMemoryContext()
        {
            Items = new List<Item>();
        }

        public List<Item> GetAllItems()
        {
            return Items;
        }

        public bool Insert(Item item)
        {

            try
            {
                Items.Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
