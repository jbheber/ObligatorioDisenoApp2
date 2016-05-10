using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class StockExceptions : Exception
    {
        public StockExceptions()
            : base()
        {
        }

        public StockExceptions(String message)
            : base(message)
        {
        }

        public StockExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

  
