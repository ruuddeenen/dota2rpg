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
        public List<BlockedUserInfo> BlockedUsersInfo;

        public AdminManageUsersViewModel()
        {
            userRepo = new ApplicationUserRepository(new ApplicationUserSQLContext());
            Users = userRepo.GetAllUsers();
            BlockedUsersInfo = userRepo.GetAllBlockedUsersInfo();
        }

        public bool IsBlocked(ApplicationUser user)
        {
            return userRepo.IsBlocked(user);
        }
    }
}
