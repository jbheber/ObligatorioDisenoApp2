using Stockapp.Data;
using Stockapp.Data.Exceptions;
using Stockapp.Data.Extensions;
using Stockapp.Data.Repository;
using Stockapp.Logic.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stockapp.Logic.Implementation
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWork UnitOfWork;

        public UserLogic(IUnitOfWork UnitOfWork)
        {
            this.UnitOfWork = UnitOfWork;
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

        public bool IsAlphaNumeric(string word)
        {
            Regex r = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,})$");
            return r.IsMatch(word);
        }

        public bool MailIsEmpty(string email)
        {
            return (email == string.Empty) ? true : false;
        }

        public bool ValidInvitationCode(InvitationCode invitationCode)
        {
            var exisitingCode = UnitOfWork.InvitationCodeRepository.Get(i => i.Code == invitationCode.Code).isEmpty();
            return exisitingCode;
        }

        public bool ValidPasswordLenght(string password)
        {
            return password.Length < 6 ? false : true;
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
            if (!ValidInvitationCode(invitationCode))
            {
                throw new UserExceptions("El codigo de invitacion no es correcto");
            }
            if (!IsAlphaNumeric(invitationCode.Code))
            {
                throw new UserExceptions("El codigo de invitacion debe ser alpfanumerico");
            }
            if (IsInDb(user))
            {
                throw new UserExceptions("Ese usuario ya fue registrado");
            }
        }

        public void RegisterUser(User user, InvitationCode invitationCode)
        {
            try
            {
                ValidateUser(user, invitationCode);
                UnitOfWork.UserRepository.Insert(user);
                UnitOfWork.InvitationCodeRepository.Delete(invitationCode);
                UnitOfWork.Save();
            }
            catch (Exception e)
            {
                throw new UserExceptions(e.Message);
            }
        }

        public bool IsInDb(User user)
        {
            var userList = UnitOfWork.UserRepository.Get();
            if (userList.isEmpty())
                return false;
            else
                return userList.Any(x => x.Name == user.Name && x.Id != user.Id);
        }

        public User LogIn(User user)
        {
            if (IsInDb(user) == false)
                return null;
            var searchedUser = UnitOfWork.UserRepository.Get(x => x.Name == user.Name).SingleOrDefault();

            if (searchedUser.Password != searchedUser.Password)
                throw new UserExceptions("Contraseña incorrecta");

            return searchedUser;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UnitOfWork.UserRepository.Get();
        }

        public bool UpdateUser(User user)
        {
            if (IsInDb(user))
                return false;
            UnitOfWork.UserRepository.Update(user);
            UnitOfWork.Save();
            return true;
        }
    }
}
