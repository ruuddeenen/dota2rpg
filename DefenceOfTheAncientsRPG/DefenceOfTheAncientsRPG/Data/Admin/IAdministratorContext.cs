using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data.Admin
{
    public interface IAdministratorContext
    {
        /// <summary>
        /// Gets a list of all admins in the database.
        /// </summary>
        /// <returns>Returns a list containing all admins.</returns>
        List<Administrator> GetAllAdmins();

        /// <summary>
        /// Blocks an active user.
        /// </summary>
        /// <param name="user">The user to block.</param>
        /// <param name="message">Message for the blocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        bool BlockUser(ApplicationUser user, string message);


        /// <summary>
        /// Unblocks an active user
        /// </summary>
        /// <param name="user">The user to unblock.</param>
        /// <param name="message">Message for the unblocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        bool UnblockUser(ApplicationUser user, string message);
    }
}
