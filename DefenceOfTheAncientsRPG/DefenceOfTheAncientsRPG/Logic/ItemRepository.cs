using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Logic
{
    public class ItemRepository
    {
        private IItemContext context;

        public ItemRepository(IItemContext context)
        {
            this.context = context;
        }

        public bool Insert(Item item)
        {
            return context.Insert(item);
        }
    }
}
