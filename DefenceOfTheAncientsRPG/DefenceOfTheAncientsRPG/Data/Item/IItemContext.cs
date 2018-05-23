using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public interface IItemContext
    {
        bool Insert(Item item);

        List<Item> GetAllItems();
    }
}
