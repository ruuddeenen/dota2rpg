using System;
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
        public bool Insert(ApplicationUser user)
        {
            user.Password = SecurePasswordHasher.Hash(user.Password);
            return context.Insert(user);
        }

        /// <summary>
        /// Gets a list of all users in the database excluding admins.
        /// </summary>
        /// <returns>Returns a list containing all users.</returns>
        public List<ApplicationUser> GetAllUsers()
        {
            return context.GetAllUsers();
        }

        /// <summary>
        /// Fetches a user from the database.
        /// </summary>
        /// <param name="id">The id corrosponding to the user to find.</param>
        /// <returns>Returns a user with the corrosponding id.</returns>
        public ApplicationUser GetUserById(string id)
        {
            foreach (ApplicationUser user in GetAllUsers())
            {
                if (user.Id == id)
                    return user;
            }
            return null;

            // OR
            // return context.GetUserByID(id);
        }

        /// <summary>
        /// Fetches a user from the database.
        /// </summary>
        /// <param name="username">The username corrosponding to the user to find.</param>
        /// <returns>Returns a user with the corrosponding username.</returns>
        public ApplicationUser GetUserByUsername(string username)
        {
            foreach (ApplicationUser user in GetAllUsers())
            {
                if (user.Username == username)
                    return user;
            }
            return null;
        }

        public bool Edit(ApplicationUser user)
        {
            return context.Edit(user);
        }

        public bool ChangePassword(ApplicationUser user)
        {
            user.Password = SecurePasswordHasher.Hash(user.Password);
            return context.ChangePassword(user);
        }

        public bool Login(string username, string password)
        {
            if (GetUserByUsername(username) != null)
            {
                if (SecurePasswordHasher.Verify(password, GetUserByUsername(username).Password))
                {
                    if (!context.IsBlocked(GetUserByUsername(username)))
                    {
                        return true; // Throw user is blocked exception
                    }
                }
                return false; // Throw password does not match exception
            }
            return false; // Throw user does not exist exception
        }
    }
}
