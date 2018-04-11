﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Data;

namespace DefenceOfTheAncientsRPG.Logic
{
    public class ApplicationUserRepository
    {
        private IApplicationUserContext context;

        public ApplicationUserRepository(IApplicationUserContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Inserts an ApplicationUser into the database.
        /// </summary>
        /// <param name="user">The user to insert.</param>
        /// <returns> Returns true if succeeded, false if not. </returns>
        bool Insert(ApplicationUser user)
        {
            return context.Insert(user);
        }

        /// <summary>
        /// Gets a list of all users in the database excluding admins.
        /// </summary>
        /// <returns>Returns a list containing all users.</returns>
        List<ApplicationUser> GetAllUsers()
        {
            return context.GetAllUsers();
        }

        /// <summary>
        /// Gets a list of all admins in the database.
        /// </summary>
        /// <returns>Returns a list containing all admins.</returns>
        List<ApplicationUser> GetAllAdmins()
        {
            return context.GetAllAdmins();
        }

        /// <summary>
        /// Fetches a user from the database.
        /// </summary>
        /// <param name="id">The id corrosponding to the user to find.</param>
        /// <returns>Returns a user with the corrosponding id.</returns>
        ApplicationUser GetUserById(string id)
        {
            return context.GetUserById(id);
        }
    }
}
