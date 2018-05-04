using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class PasswordDoesNotContainNumberException : Exception
    {

        public PasswordDoesNotContainNumberException()
        {

        }

        public PasswordDoesNotContainNumberException(string message)
            : base(message)
        {

        }

        public PasswordDoesNotContainNumberException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
