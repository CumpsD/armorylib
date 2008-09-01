using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmoryLib.Exceptions
{
    public class InvalidRegionException : Exception
    {
        public InvalidRegionException() { }
        public InvalidRegionException(string message) : base(message) { }
        public InvalidRegionException(string message, Exception inner) : base(message, inner) { }
    }
}
