using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class UserException : Exception
    {
        public UserException()
            : base()
        {
        }

        public UserException(String message)
            : base(message)
        {
        }

        public UserException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
