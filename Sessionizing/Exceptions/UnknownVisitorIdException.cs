using System;

namespace Sessionizing.Exceptions
{
    public class UnknownVisitorIdException : Exception
    {
        public UnknownVisitorIdException(string message)
            :base(message)
        {
        }
    }
}