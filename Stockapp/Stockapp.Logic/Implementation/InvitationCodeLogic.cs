using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Data.Extensions;
using Stockapp.Data.Exceptions;

namespace Stockapp.Logic.Implementation
{
    public class InvitationCodeLogic : IInvitationCodeLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public InvitationCodeLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public InvitationCode GenerateCode(User administator)
        {
            if (administator.IsAdmin == false)
                throw new InvitationCodeExceptions("Solo administaradores pueden generar codigos");

            var exisitngCodes = UnitOfWork.InvitationCodeRepository.Get();

            var newInvitationCode = new InvitationCode()
            {
                Code = RandomAlphanumericString(8),
                ParentUser = administator
            };

            while (exisitngCodes.isNotEmpty() && exisitngCodes.Any(c => c.Code == newInvitationCode.Code))
                newInvitationCode.Code = RandomAlphanumericString(8);

            UnitOfWork.InvitationCodeRepository.Insert(newInvitationCode);
            UnitOfWork.Save();

            return newInvitationCode;
        }

        /// <summary>
        /// Generates a random alphanumeric code
        /// </summary>
        /// <param name="length">Length of the code</param>
        /// <returns></returns>
        private string RandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
