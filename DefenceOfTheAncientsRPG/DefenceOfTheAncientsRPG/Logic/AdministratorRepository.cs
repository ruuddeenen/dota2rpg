using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Data.Admin;

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
        List<Administrator> GetAllAdmins()
        {
            return context.GetAllAdmins();
        }

        /// <summary>
        /// Blocks an active user.
        /// </summary>
        /// <param name="user">The user to block.</param>
        /// <param name="message">Message for the blocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        bool BlockUser(ApplicationUser user, string message)
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
        bool UnblockUser(ApplicationUser user, string message)
        {
            if (!user.Active)
            {
                return context.UnblockUser(user, message);
            }
            return false;
        }
    }
}
