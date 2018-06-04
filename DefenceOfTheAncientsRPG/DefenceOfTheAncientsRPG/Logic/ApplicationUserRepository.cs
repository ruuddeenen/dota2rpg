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
            return context.GetUserById(id);
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
            ApplicationUser user = GetUserByUsername(username);
            if (SecurePasswordHasher.Verify(password, user.Password))
            {
                if (!IsBlocked(user))
                {
                    return true;
                }
                throw new UserIsBlockedException();
            }
            throw new IncorrectPasswordException();
        }

    public bool IsBlocked(ApplicationUser user)
    {
        RefreshBlockedUsers();
        return context.IsBlocked(user);
    }

    /// <summary>
    /// Removes old entries from the BlockedUsers context.
    /// </summary>
    /// <returns>The amount of entries removed.</returns>
    private int RefreshBlockedUsers()
    {
        List<BlockedUserInfo> toBeRemoved = new List<BlockedUserInfo>();

        foreach (BlockedUserInfo bui in GetAllBlockedUsersInfo())
        {
            if (bui.Until > DateTime.Now)
            {
                toBeRemoved.Add(bui);
            }
        }
        if (toBeRemoved.Count > 0)
        {
            return context.RemoveEntriesFromBlockedUsers(toBeRemoved);
        }
        else return 0;
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
        foreach (BlockedUserInfo bui in GetAllBlockedUsersInfo())
        {
            if (bui.UserId == userId)
            {
                return context.Unblock(userId);
            }
        }
        throw new EntryDoesNotExistException(string.Format("User with userId: {0} does not exist in the context", userId));
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

    public BlockedUserInfo GetBlockedUserInfoByUsername(string username)
    {
        foreach (BlockedUserInfo bui in GetAllBlockedUsersInfo())
        {
            if (bui.UserId == GetUserByUsername(username).Id)
            {
                return bui;
            }
        }
        throw new EntryDoesNotExistException(string.Format("No blocked user with username: {0} exists in the context.", username));
    }

    public List<BlockedUserInfo> GetAllBlockedUsersInfo()
    {
        return context.GetAllBlockedUsersInfo();
    }

    public BlockedUserInfo GetBlockedUserInfoByUserId(string userId)
    {
        foreach (BlockedUserInfo bui in GetAllBlockedUsersInfo())
        {
            if (bui.UserId == userId)
            {
                return bui;
            }
        }
        throw new EntryDoesNotExistException(string.Format("No blocked user with userId: {0} exists in the context.", userId));
    }
}
}
