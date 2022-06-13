using System;

namespace Workshop.Web.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string message) : base(message)
        {
        }
    }
}