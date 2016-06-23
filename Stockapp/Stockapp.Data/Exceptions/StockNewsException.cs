using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class StockNewsException : Exception
    {
        public StockNewsException()
            : base()
        {
        }

        public StockNewsException(String message)
            : base(message)
        {
        }

        public StockNewsException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


 