using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class PortfolioExceptions : Exception
    {
        public PortfolioExceptions()
            : base()
        {
        }

        public PortfolioExceptions(String message)
            : base(message)
        {
        }

        public PortfolioExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}



