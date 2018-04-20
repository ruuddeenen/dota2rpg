using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
namespace DefenceOfTheAncientsRPG.Data
{
    public interface IApplicationUserContext
    {
        /// <summary>
        /// Inserts an ApplicationUser into the database.
        /// </summary>
        /// <param name="user">The user to insert.</param>
        /// <returns>Returns true if succeeded, false if not.</returns>
        bool Insert(ApplicationUser user);

        /// <summary>
        /// Edit a user's email, first and last name.
        /// </summary>
        /// <param name="user">The edited user.</param>
        /// <returns>Returns true if succeeded, false if not.</returns>
        bool Edit(ApplicationUser user);

        /// <summary>
        /// Gets a list of all users in the database excluding admins.
        /// </summary>
        /// <returns>Returns a list containing all users.</returns>
        List<ApplicationUser> GetAllUsers();


        /// <summary>
        /// Fetches a user from the database.
        /// </summary>
        /// <param name="id">The id corrosponding to the user to find.</param>
        /// <returns>Returns a user with the corrosponding id.</returns>
        ApplicationUser GetUserById(string id);

        /// <summary>
        /// Fetches a user from the database.
        /// </summary>
        /// <param name="username">The username corrosponding to the user to find.</param>
        /// <returns>Returns a user with the corrosponding username.</returns>
        ApplicationUser GetUserByUsername(string username);

        /// <summary>
        /// Changes the password of a user
        /// </summary>
        /// <param name="user">The user with the already changed password.</param>
        /// <returns>Return true if succeeded, false if failed.</returns>
        bool ChangePassword(ApplicationUser user);

        bool IsBlocked(ApplicationUser user);
    }
}
