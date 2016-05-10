using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IInvitationCodeLogic
    {
        /// <summary>
        /// Generates a random unique alphanumeric code.
        /// </summary>
        /// <param name="administator">The user that is requesting the code</param>
        /// <returns></returns>
        InvitationCode GenerateCode(User administator);
    }
}
