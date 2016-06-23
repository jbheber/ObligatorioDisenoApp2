using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class StockHistoryException : Exception
    {
        public StockHistoryException()
            : base()
        {
        }

        public StockHistoryException(String message)
            : base(message)
        {
        }

        public StockHistoryException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}



