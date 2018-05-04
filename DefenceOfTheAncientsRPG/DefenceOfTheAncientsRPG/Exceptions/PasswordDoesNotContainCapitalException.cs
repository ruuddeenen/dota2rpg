using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class PasswordDoesNotContainCapitalException : Exception
    {

        public PasswordDoesNotContainCapitalException()
        {

        }

        public PasswordDoesNotContainCapitalException(string message)
            : base(message)
        {

        }

        public PasswordDoesNotContainCapitalException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
