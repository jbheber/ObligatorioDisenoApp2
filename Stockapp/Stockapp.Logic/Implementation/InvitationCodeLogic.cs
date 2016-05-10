using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;

namespace Stockapp.Logic.Implementation
{
    public class InvitationCodeLogic : IInvitationCodeLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public InvitationCodeLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public InvitationCode GenerateCode(User administator)
        {
            throw new NotImplementedException();
        }
    }
}
