using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stockapp.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public UserLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
        }

        public bool IsAlphaNumeric(string word)
        {
            Regex r = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,})$");
            return r.IsMatch(word);
        }

        public bool MailIsEmpty(string email)
        {
            return (email == string.Empty) ? true : false;
        }

        public bool ValidInvitationCodeLenght(string invitationCode)
        {
            return (invitationCode.Length != 8) ? false : true;
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
            return password.Length < 6 ? false : true;
        }

        public void RegisterUser(User user, InvitationCode invitationCode)
        {
            throw new NotImplementedException();
        }

        public void ValidateUser(User user, InvitationCode invitationCode)
        {
            if (!EmailIsUnique(user.Email))
            {
                throw new UserExceptions("El email ya esta en uso");
            }
            if (MailIsEmpty(user.Email))
            {
                throw new UserExceptions("El email no puede ser vacio");
            }
            if (!ValidPasswordLenght(user.Password))
            {
                throw new UserExceptions("El largo de la contraseña debe ser mayor o igual que 6");
            }
            if (!IsAlphaNumeric(user.Password))
            {
                throw new UserExceptions("La contraseña debe ser alpfanumerica");
            }
            if (!ValidInvitationCodeLenght(invitationCode.Code))
            {
                throw new UserExceptions("El largo del codigo de invitacion debe ser 8");
            }
            if (!IsAlphaNumeric(invitationCode.Code))
            {
                throw new UserExceptions("El codigo de invitacion debe ser alpfanumerico");
            }
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
