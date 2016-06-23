using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class PlayerException : Exception
    {
        public PlayerException()
            : base()
        {
        }

        public PlayerException(String message)
            : base(message)
        {
        }

        public PlayerException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


