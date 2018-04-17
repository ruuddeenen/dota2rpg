using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Logic;

namespace DefenceOfTheAncientsRPG.Models.AdminViewModel
{
    public class AdminManageUsersViewModel
    {
        private ApplicationUserRepository userRepo; 
        public List<ApplicationUser> Users;

        public AdminManageUsersViewModel()
        {
            userRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());
            Users = userRepo.GetAllUsers();
        }
    }
}
