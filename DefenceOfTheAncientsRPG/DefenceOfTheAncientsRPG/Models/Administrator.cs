using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Models
{
    public class Administrator
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:YYYYMMDD")]
        public DateTime DateOfBirth { get; set; }

        public Administrator()
        {

        }

        public Administrator(string password, string firstName, string lastName)
        {
            ID = Guid.NewGuid().ToString();
            Username = CreateUsername(firstName, lastName);
            PasswordHash = SecurePasswordHasher.Hash(password);
            FirstName = firstName;
            LastName = lastName;
        }

        private string CreateUsername(string fn, string ln)
        {
            return string.Format("a.{0}{1}", fn, ln);
        }
    }
}
