using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Data;

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
        public List<Administrator> GetAllAdmins()
        {
            return context.GetAllAdmins();
        }

        /// <summary>
        /// Blocks an active user.
        /// </summary>
        /// <param name="user">The user to block.</param>
        /// <param name="message">Message for the blocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        public bool BlockUser(ApplicationUser user, string message)
        {
            if (user.Active)
            {
                return context.BlockUser(user, message);
            }
            return false;
        }


        /// <summary>
        /// Unblocks an active user
        /// </summary>
        /// <param name="user">The user to unblock.</param>
        /// <param name="message">Message for the unblocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        public bool UnblockUser(ApplicationUser user, string message)
        {
            if (!user.Active)
            {
                return context.UnblockUser(user, message);
            }
            return false;
        }

        /// <summary>
        /// Creates an administrator account.
        /// </summary>
        /// <param name="admin">The administrator to create the account from.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        public bool Insert(Administrator admin)
        {
            return context.Insert(admin);
        }


        /// <summary>
        /// Fetches an admin from the database.
        /// </summary>
        /// <param name="id">The id corrosponding to the admin</param>
        /// <returns>True is succeeded, false if failed or admin not found.</returns>
        public Administrator GetAdminById(string id)
        {
            return context.GetAdminById(id);
        }


        /// <summary>
        /// Fetches a admin from the database.
        /// </summary>
        /// <param name="username">The username corrosponding to the admin to find.</param>
        /// <returns>Returns a admin with the corrosponding username.</returns>
        public Administrator GetAdminByUsername(string username)
        {
            return context.GetAdminByUsername(username);
        }
    }
}
