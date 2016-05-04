using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class UserExceptions : Exception
    {
        public UserExceptions()
            : base()
        {
        }

        public UserExceptions(String message)
            : base(message)
        {
        }

        public UserExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
