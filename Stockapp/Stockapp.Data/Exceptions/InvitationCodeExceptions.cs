using System;

namespace Stockapp.Data.Exceptions
{
    [SerializableAttribute]
    public class InvitationCodeExceptions : Exception
    {
        public InvitationCodeExceptions()
            : base()
        {
        }

        public InvitationCodeExceptions(String message)
            : base(message)
        {
        }

        public InvitationCodeExceptions(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


