using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefenceOfTheAncientsRPG.Exceptions
{
    public class FileFormatException : Exception
    {
        public FileFormatException()
        {

        }

        public FileFormatException(string message)
            : base(message)
        {

        }

        public FileFormatException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
