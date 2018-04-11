using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ApplicationUserSQLContext : IApplicationUserContext
    {
        public List<ApplicationUser> GetAllAdmins()
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
