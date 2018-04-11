using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DefenceOfTheAncientsRPG.Models
{
    public class ApplicationUser
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(bool admin, string username, string password)
        {
            ID =  Guid.NewGuid().ToString();
            Username = username;
            PasswordHash = SecurePasswordHasher.Hash(password);
            FirstName = null;
            LastName = null;
            CreatedOn = DateTime.Now;
            Active = true;
            Admin = admin;
        }

        public ApplicationUser(bool admin, string username, string password, string firstName)
        {
            ID = Guid.NewGuid().ToString();
            Username = username;
            PasswordHash = SecurePasswordHasher.Hash(password);
            FirstName = firstName;
            LastName = null;
            CreatedOn = DateTime.Now;
            Active = true;
            Admin = admin;
        }

        public ApplicationUser(bool admin, string username, string password, string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            Username = username;
            PasswordHash = SecurePasswordHasher.Hash(password);
            FirstName = firstName;
            LastName = lastName;
            CreatedOn = DateTime.Now;
            Active = true;
            Admin = admin;
        }
    }
}
