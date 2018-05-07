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
        private List<Administrator> Admins;
        private List<ApplicationUser> Users;

        public AdministratorMemoryContext()
        {
            Administrator superUser = new Administrator
            {
                ID = Guid.NewGuid().ToString(),
                Username = "a.SuperUser",
                Password = Logic.SecurePasswordHasher.Hash("SU@123"),
                FirstName = "Super",
                LastName = "User",
                DateOfBirth = DateTime.Now,
                CreatedOn = DateTime.Now,
                Activated = true
            };
            Admins = new List<Administrator>
            {
                superUser
            };
            Users = new List<ApplicationUser>();
            
        }

        public bool BlockUser(bool block, ApplicationUser userToBlock, string message)
        {
            foreach(ApplicationUser user in Users)
            {
                if (userToBlock.Id == user.Id)
                {
                    if (block) user.Active = false;
                    else user.Active = true;
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
            try
            {
                Admins.Add(admin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangePassword(Administrator admin)
        {
            throw new NotImplementedException();
        }

        public bool Activate(Administrator admin)
        {
            throw new NotImplementedException();
        }

        public bool BlockUser(BlockedUserInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
