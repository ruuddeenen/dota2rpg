using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class AdministratorMemoryContext : IAdministratorContext
    {
        private List<Administrator> Admins = new List<Administrator>();
        private List<ApplicationUser> Users = new List<ApplicationUser>();

        public bool BlockUser(ApplicationUser userToBlock, string message)
        {
            foreach(ApplicationUser user in Users)
            {
                if (userToBlock.ID == user.ID)
                {
                    if (user.Active)
                    {
                        user.Active = false;
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Administrator> GetAllAdmins()
        {
            return Admins;
        }

        public bool Insert(Administrator admin)
        {
            if (GetAdminById(admin.ID) == null)
            {
                Admins.Add(admin);
                return true;
            }
            return false;
        }


        public Administrator GetAdminById(string id)
        {
            foreach (Administrator admin in Admins)
            {
                if (admin.ID == id)
                {
                    return admin;
                }
            }
            return null;
        }

        public bool UnblockUser(ApplicationUser userToUnblock, string message)
        {
            foreach (ApplicationUser user in Users)
            {
                if (userToUnblock.ID == user.ID)
                {
                    if (!user.Active)
                    {
                        user.Active = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public Administrator GetAdminByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
