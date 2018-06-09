using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class PasswordFormatException : Exception
    {

        public PasswordFormatException()
        {

        }

        public PasswordFormatException(string message)
            : base(message)
        {

        }

        public PasswordFormatException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
