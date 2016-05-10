using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class StockNewsExceptions : Exception
    {
        public StockNewsExceptions()
            : base()
        {
        }

        public StockNewsExceptions(String message)
            : base(message)
        {
        }

        public StockNewsExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


 