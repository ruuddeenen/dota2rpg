using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefenceOfTheAncientsRPG.Models;
using DefenceOfTheAncientsRPG.Data;
using DefenceOfTheAncientsRPG.Exceptions;

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
        /// Creates an administrator account.
        /// </summary>
        /// <param name="admin">The administrator to create the account from.</param>
        /// <returns>True if succeeded, false if failed.</returns>
        public bool Insert(Administrator admin)
        {
            foreach (Administrator a in GetAllAdmins())
            {
                if (a.Id == admin.Id)
                {
                    throw new EntryAlreadyExistsException();
                }
            }
            return context.Insert(admin);
        }


        /// <summary>
        /// Fetches an admin from the database.
        /// </summary>
        /// <param name="id">The id corrosponding to the admin</param>
        /// <returns>True is succeeded, false if failed or admin not found.</returns>
        public Administrator GetAdminById(string id)
        {
            foreach (Administrator admin in GetAllAdmins())
            {
                if (admin.Id == id)
                {
                    return admin;
                }
            }
            throw new EntryDoesNotExistException();
        }


        /// <summary>
        /// Fetches a admin from the database.
        /// </summary>
        /// <param name="username">The username corrosponding to the admin to find.</param>
        /// <returns>Returns a admin with the corrosponding username.</returns>
        public Administrator GetAdminByUsername(string username)
        {
            foreach (Administrator admin in GetAllAdmins())
            {
                if (admin.Username == username)
                {
                    return admin;
                }
            }
            throw new EntryDoesNotExistException();
        }


        /// <summary>
        /// Changes the password of a admin
        /// </summary>
        /// <param name="admin">The admin id</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>Return true if succeeded, false if failed.</returns>
        public bool ChangePassword(string adminid, string newPassword)
        {
            Administrator admin = GetAdminById(adminid);
            if (PasswordChecker(newPassword))
            {
                if (!admin.Activated)
                {
                    Activate(admin);
                }
                return context.ChangePassword(adminid, SecurePasswordHasher.Hash(newPassword));

            }
            else return false;
        }

        /// <summary>
        /// Activates an admin account
        /// </summary>
        /// <param name="admin">The admin to activate</param>
        /// <returns>Return true if succeeded, false if failed.</returns>
        public bool Activate(Administrator admin)
        {
            return context.Activate(admin);
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

        public bool Login(string username, string password)
        {
            if (GetAdminByUsername(username) != null)
            {
                if (SecurePasswordHasher.Verify(password, GetAdminByUsername(username).Password))
                {
                    return true;
                }
                throw new IncorrectPasswordException();
            }
            throw new EntryDoesNotExistException();
        }

        public string GetFullNameByAdminId(string id)
        {
            Administrator admin = GetAdminById(id);
            return admin.FirstName + " " + admin.LastName;
        }
    }
}
