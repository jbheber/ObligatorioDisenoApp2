using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class TransactionExceptions : Exception
    {
        public TransactionExceptions()
            : base()
        {
        }

        public TransactionExceptions(String message)
            : base(message)
        {
        }

        public TransactionExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


