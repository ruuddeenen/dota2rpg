﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ApplicationUserMemoryContext : IApplicationUserContext
    {
        private List<ApplicationUser> Users = new List<ApplicationUser>();

        public bool Edit(ApplicationUser editedUser)
        {
            foreach (ApplicationUser user in Users)
            {
                if (user.ID == editedUser.ID)
                {
                    user.FirstName = editedUser.FirstName;
                    user.LastName = editedUser.LastName;
                    user.Email = editedUser.Email;
                    return true;
                }
            }
            return false;
        }


        public List<ApplicationUser> GetAllUsers()
        {
            return Users;
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

        public ApplicationUser GetUserByUsername(string username)
        {
            foreach (ApplicationUser user in Users)
            {
                if (user.Username == username)
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
