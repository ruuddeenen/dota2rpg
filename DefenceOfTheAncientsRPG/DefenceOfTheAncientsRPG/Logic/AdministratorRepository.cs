using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Data.Admin;

namespace DefenceOfTheAncientsRPG.Logic
{
    public class AdministratorRepository
    {
        private IAdministratorContext context;

        public AdministratorRepository(IAdministratorContext context)
        {
            this.context = context;
        }


        /// <summary>
        /// Gets a list of all admins in the database.
        /// </summary>
        /// <returns>Returns a list containing all admins.</returns>
        List<Administrator> GetAllAdmins()
        {
            return context.GetAllAdmins();
        }
    }
}
