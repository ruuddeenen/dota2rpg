using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class EntryDoesNotExistException : Exception
    {
        public EntryDoesNotExistException()
        {

        }

        public EntryDoesNotExistException(string message)
            : base(message)
        {

        }

        public EntryDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
