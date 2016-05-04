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

        public bool IsAlphaNumeric(string word)
        {
            throw new NotImplementedException();
        }

        public bool MailIsEmpty(string email)
        {
            throw new NotImplementedException();
        }

        public bool ValidInvitationCodeLenght(string invitationCode)
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

        public bool ValidPasswordLenght(string password)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(User user, InvitationCode invitationCode)
        {
            throw new NotImplementedException();
        }

        public void ValidateUser(User user, InvitationCode invitationCode)
        {
            throw new NotImplementedException();
        }

        public bool EmailIsUnique(string email)
        {
            var userList = UnitOfWork.UserRepository.Get();
            if (userList.Any(u => u.Email == email))
            {
                return false;
            }
            return true;
        }
    }
}
