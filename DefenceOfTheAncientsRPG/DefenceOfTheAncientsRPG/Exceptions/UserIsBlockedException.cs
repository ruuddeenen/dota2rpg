using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class UserIsBlockedException : Exception
    {
        public UserIsBlockedException()
        {

        }

        public UserIsBlockedException(string message)
            : base(message)
        {

        }

        public UserIsBlockedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
