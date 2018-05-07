using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class EntryAlreadyExistsException : Exception
    {

        public EntryAlreadyExistsException()
        {

        }

        public EntryAlreadyExistsException(string message)
            : base(message)
        {

        }

        public EntryAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
