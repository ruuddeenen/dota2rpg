using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data.Admin
{
    public class AdministratorMemoryContext
    {
        private List<Administrator> Admins = new List<Administrator>();

        public List<Administrator> GetAllAdmins()
        {
            return Admins;
        }
    }
}
