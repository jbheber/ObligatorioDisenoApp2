using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class StockHistoryExceptions : Exception
    {
        public StockHistoryExceptions()
            : base()
        {
        }

        public StockHistoryExceptions(String message)
            : base(message)
        {
        }

        public StockHistoryExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}



