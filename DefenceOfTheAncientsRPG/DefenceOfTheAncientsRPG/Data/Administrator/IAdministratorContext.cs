using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;

namespace DefenceOfTheAncientsRPG.Data
{
    public interface IAdministratorContext
    {
        /// <summary>
        /// Gets a list of all admins in the database.
        /// </summary>
        /// <returns>Returns a list containing all admins.</returns>
        List<Administrator> GetAllAdmins();


        /// <summary>
        /// Creates an administrator account.
        /// </summary>
        /// <param name="admin">The administrator to create the account from.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        bool Insert(Administrator admin);

        /// <summary>
        /// Changes the password of a admin
        /// </summary>
        /// <param name="admin">The admin with the already changed password.</param>
        /// <returns>Return true if succeeded, false if failed.</returns>
        bool ChangePassword(Administrator admin);

        /// <summary>
        /// Activates an admin account
        /// </summary>
        /// <param name="admin">The admin to activate</param>
        /// <returns>Return true if succeeded, false if failed.</returns>
        bool Activate(Administrator admin);
    }
}
