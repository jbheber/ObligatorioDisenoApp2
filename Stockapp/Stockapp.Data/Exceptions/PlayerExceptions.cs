using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class PlayerExceptions : Exception
    {
        public PlayerExceptions()
            : base()
        {
        }

        public PlayerExceptions(String message)
            : base(message)
        {
        }

        public PlayerExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


