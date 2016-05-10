using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class AdminExceptions : Exception
    {
        public AdminExceptions()
            : base()
        {
        }

        public AdminExceptions(String message)
            : base(message)
        {
        }

        public AdminExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
