using Stockapp.Data;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using System;
using System.Linq;

namespace Stockapp.Logic
{
    public class UserLogic: IUserLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public UserLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public bool AlphaNumeric(string word)
        {
            throw new NotImplementedException();
        }

        public bool EmptyMail(string email)
        {
            throw new NotImplementedException();
        }

        public bool InvitationCodeLenght(string invitationCode)
        {
            throw new NotImplementedException();
        }

        public void IsInDb(User user)
        {
            throw new NotImplementedException();
        }

        public User LogIn(User user)
        {
            throw new NotImplementedException();
        }

        public bool PasswordLenght(string password)
        {
            throw new NotImplementedException();
        }

        public void Register(User user, InvitationCode invitationCode)
        {
            throw new NotImplementedException();
        }

        public void RegisterValidations(User user, InvitationCode invitationCode)
        {
            throw new NotImplementedException();
        }

        public bool UniqueUserEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
