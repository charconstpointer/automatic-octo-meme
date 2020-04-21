using System;

namespace GitHub.Exceptions
{
    public class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException(string message):base(message)
        {
            
        }
    }
}