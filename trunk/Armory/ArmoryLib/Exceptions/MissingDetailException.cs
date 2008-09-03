using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Exceptions
{
    public class MissingDetailException : Exception
    {
        public MissingDetailException() { }
        public MissingDetailException(string message) : base(message) { }
        public MissingDetailException(string message, Exception inner) : base(message, inner) { }
    }
}
