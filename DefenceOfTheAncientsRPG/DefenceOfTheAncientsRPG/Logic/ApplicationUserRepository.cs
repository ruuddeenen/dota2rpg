using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Exceptions;

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
            try
            {
                GetUserByUsername(user.Username);
            }
            catch (EntryDoesNotExistException)
            {
                if (PasswordChecker(user.Password))
                {
                    user.Password = SecurePasswordHasher.Hash(user.Password);
                    return context.Insert(user);
                }
            }
            throw new EntryAlreadyExistsException("Username already exists.");
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
            throw new EntryDoesNotExistException();
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
            throw new EntryDoesNotExistException();
        }
        /// <summary>
        /// Edits a user's first name, last name and email address.
        /// </summary>
        /// <param name="user">The edited user.</param>
        /// <returns>Returns true if succeeded, false if failed.</returns>
        public bool Edit(ApplicationUser user)
        {
            return context.Edit(user);
        }

        public bool ChangePassword(string userid, string newPassword)
        {
            if (PasswordChecker(newPassword))
            {
                newPassword = SecurePasswordHasher.Hash(newPassword);
                return context.ChangePassword(userid, newPassword);
            }
            else return false;
        }

        /// <summary>
        /// Tries to log a user in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password to match.</param>
        /// <returns>True if username and password match. Error otherwise.</returns>
        public bool Login(string username, string password)
        {
            if (GetUserByUsername(username) != null)
            {
                if (SecurePasswordHasher.Verify(password, GetUserByUsername(username).Password))
                {
                    if (context.IsBlocked(GetUserByUsername(username)))
                    {
                        throw new UserIsBlockedException();
                    }
                    return true;
                }
                throw new IncorrectPasswordException();
            }
            throw new EntryDoesNotExistException();
        }

        /// <summary>
        /// Blocks an active user.
        /// </summary>
        /// <param name="user">The user to block.</param>
        /// <param name="message">Message for the blocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        public bool BlockUser(BlockedUserInfo info)
        {
            return context.BlockUser(info);
        }


        /// <summary>
        /// Unblocks an active user
        /// </summary>
        /// <param name="user">The user to unblock.</param>
        /// <param name="message">Message for the unblocked user.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        public bool UnblockUser(string userId)
        {
            return context.Unblock(userId);
        }

        private bool PasswordChecker(string password)
        {
            if (password.Any(c => char.IsLower(c)))
            {
                if (password.Any(c => char.IsUpper(c)))
                {
                    if (password.Any(c => char.IsNumber(c)))
                    {
                        return true;
                    }
                    else throw new PasswordFormatException("Password does not contain a numeric character.");
                }
                else throw new PasswordFormatException("Password does not contain an upper case character.");
            }
            else throw new PasswordFormatException("Password does not contain a lower case character.");
        }

        public BlockedUserInfo GetBlockedUserInfoByUserId(string userId)
        {
            return context.GetBlockedUserInfo(userId);
        }
    }
}
