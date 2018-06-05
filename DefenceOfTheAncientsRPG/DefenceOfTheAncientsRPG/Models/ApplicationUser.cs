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
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:YYYYMMDD")]
        public DateTime CreatedOn { get; set; }
        public bool Blocked { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(string username, string password, string email, string firstName, string lastName)
        {
            Id = Guid.NewGuid().ToString();
            Email = email;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            CreatedOn = DateTime.Now.Date;
            Blocked = false;
        }
    }
}
