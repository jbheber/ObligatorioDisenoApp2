using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class InvitationCodeException : Exception
    {
        public InvitationCodeException()
            : base()
        {
        }

        public InvitationCodeException(String message)
            : base(message)
        {
        }

        public InvitationCodeException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


