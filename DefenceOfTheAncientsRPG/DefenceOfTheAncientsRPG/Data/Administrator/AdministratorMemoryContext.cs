﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Logic;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Exceptions;

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

        public bool ChangePassword(string adminId, string newPassword)
        {
            foreach (Administrator admin in Admins)
            {
                if (admin.ID == adminId)
                {
                    admin.Password = newPassword;
                    return true;
                }
            }
            throw new EntryDoesNotExistException();
        }

        public bool Activate(Administrator admin)
        {
            foreach (Administrator Admin in Admins)
            {
                if (admin.ID == Admin.ID)
                {
                    admin.Activated = true;
                    return true;
                }
            }
            throw new EntryDoesNotExistException();
        }
    }
}
