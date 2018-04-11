using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ApplicationUserMemoryContext : IApplicationUserContext
    {
        private List<ApplicationUser> Users = new List<ApplicationUser>();

        public List<ApplicationUser> GetAllAdmins()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (ApplicationUser user in Users)
            {
                if (user.Admin)
                    users.Add(user);
            }
            return users;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach(ApplicationUser user in Users)
            {
                if (!user.Admin)
                    users.Add(user);
            }
            return users;
        }

        public ApplicationUser GetUserById(string id)
        {
            foreach (ApplicationUser user in Users)
            {
                if (user.ID == id)
                {
                    return user;
                }
            }
            return null;
        }

        public bool Insert(ApplicationUser user)
        {
            if (GetUserById(user.ID) == null)
            {
                Users.Add(user);
                return true;
            }
            return false;
        }
    }
}
