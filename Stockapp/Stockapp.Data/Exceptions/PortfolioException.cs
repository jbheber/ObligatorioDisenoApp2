using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class PortfolioException : Exception
    {
        public PortfolioException()
            : base()
        {
        }

        public PortfolioException(String message)
            : base(message)
        {
        }

        public PortfolioException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}



