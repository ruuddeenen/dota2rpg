using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Exceptions;

namespace DefenceOfTheAncientsRPG.Data
{
    public class ApplicationUserMemoryContext : IApplicationUserContext
    {
        private List<ApplicationUser> Users = new List<ApplicationUser>();
        private List<BlockedUserInfo> BlockedUsersInfo = new List<BlockedUserInfo>();

        public ApplicationUserMemoryContext()
        {
        }

        public bool BlockUser(BlockedUserInfo info)
        {
            foreach (BlockedUserInfo bui in BlockedUsersInfo)
            {
                if (info.UserId == bui.UserId)
                {
                    throw new EntryAlreadyExistsException("An entry for this userId already exist");
                }
            }
            BlockedUsersInfo.Add(info);
            return true;
        }

        public bool ChangePassword(string userid, string newpassword)
        {
            foreach (ApplicationUser user in Users)
            {
                if (userid == user.Id)
                {
                    user.Password = newpassword;
                    return true;
                }
            }
            throw new EntryDoesNotExistException();
        }

        public bool Edit(ApplicationUser editedUser)
        {
            foreach (ApplicationUser user in Users)
            {
                if (user.Id == editedUser.Id)
                {
                    user.FirstName = editedUser.FirstName;
                    user.LastName = editedUser.LastName;
                    user.Email = editedUser.Email;
                    return true;
                }
            }
            throw new EntryDoesNotExistException();
        }

        public List<BlockedUserInfo> GetAllBlockedUsersInfo()
        {
            return BlockedUsersInfo;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return Users;
        }

        public ApplicationUser GetUserById(string id)
        {
            foreach (ApplicationUser user in Users)
            {
                if (user.Id == id)
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
            if (GetUserById(user.Id) == null)
            {
                Users.Add(user);
                return true;
            }
            return false;
        }

        public bool IsBlocked(ApplicationUser user)
        {
            foreach (BlockedUserInfo info in BlockedUsersInfo)
            {
                if (info.UserId == user.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Unblock(string userId)
        {
            foreach (BlockedUserInfo info in BlockedUsersInfo)
            {
                if (userId == info.UserId)
                {
                    BlockedUsersInfo.Remove(info);
                    return true;
                }
            }
            throw new EntryDoesNotExistException("An entry with given userId does not exist");
        }
    }
}
