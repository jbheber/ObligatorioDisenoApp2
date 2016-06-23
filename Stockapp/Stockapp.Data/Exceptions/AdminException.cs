using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class AdminException : Exception
    {
        public AdminException()
            : base()
        {
        }

        public AdminException(String message)
            : base(message)
        {
        }

        public AdminException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
