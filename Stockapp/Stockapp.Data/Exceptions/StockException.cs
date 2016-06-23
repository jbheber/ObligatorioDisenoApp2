using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class StockException : Exception
    {
        public StockException()
            : base()
        {
        }

        public StockException(String message)
            : base(message)
        {
        }

        public StockException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

  
