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
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:YYYYMMDD")]
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(bool admin, string username, string password, string email, string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            Username = username;
            PasswordHash = SecurePasswordHasher.Hash(password);
            FirstName = firstName;
            LastName = lastName;
            CreatedOn = DateTime.Now.Date;
            Active = true;
            Admin = admin;
        }
    }
}
