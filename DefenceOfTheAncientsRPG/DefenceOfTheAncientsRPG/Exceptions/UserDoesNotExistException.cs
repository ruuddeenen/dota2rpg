using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException()
        {

        }

        public UserDoesNotExistException(string message)
            : base(message)
        {

        }

        public UserDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
