using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class TransactionException : Exception
    {
        public TransactionException()
            : base()
        {
        }

        public TransactionException(String message)
            : base(message)
        {
        }

        public TransactionException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


