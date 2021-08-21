using System;

namespace Sessionizing.Exceptions
{
    public class UnknownSiteUrlException : Exception
    {
        public UnknownSiteUrlException(string message)
            :base(message)
        {
        }
    }
}