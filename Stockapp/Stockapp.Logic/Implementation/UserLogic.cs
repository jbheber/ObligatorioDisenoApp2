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
            var exisitingCode = UnitOfWork.InvitationCodeRepository.Get(i => i.Code == invitationCode.Code).IsNotEmpty();
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
                throw new UserException("El email ya está en uso");
            }
            if (MailIsEmpty(user.Email))
            {
                throw new UserException("El email no puede ser vacío");
            }
            if (!ValidPasswordLenght(user.Password))
            {
                throw new UserException("El largo de la contraseña debe ser mayor o igual que 6");
            }
            if (!IsAlphaNumeric(user.Password))
            {
                throw new UserException("La contraseña debe ser alfanumérica");
            }
            if (!ValidInvitationCode(invitationCode))
            {
                throw new UserException("El codigo de invitación no es correcto");
            }
            if (!IsAlphaNumeric(invitationCode.Code))
            {
                throw new UserException("El codigo de invitación debe ser alfanumérico");
            }
        }

        public bool RegisterUser(User user, InvitationCode invitationCode)
        {
            try
            {
                ValidateUser(user, invitationCode);
                UnitOfWork.UserRepository.Insert(user);
                var invitationCodeFromDb = UnitOfWork.InvitationCodeRepository.Get(i => i.Code == invitationCode.Code).SingleOrDefault();
                UnitOfWork.InvitationCodeRepository.Delete(invitationCodeFromDb);
                UnitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                throw new UserException(e.Message);
            }
        }

        public void RegisterWindowsForm(User user)
        {
            UnitOfWork.UserRepository.Insert(user);
            UnitOfWork.Save();
        }

        public bool IsInDb(User user)
        {
            var userList = UnitOfWork.UserRepository.Get();
            if (userList.IsEmpty())
                return false;
            else
                return userList.Any(x => x.Email == user.Email && x.Id != user.Id);
        }

        public User LogIn(User user)
        {
            if (!IsInDb(user))
                return null;
            var searchedUser = UnitOfWork.UserRepository.Get(x => x.Email == user.Email).SingleOrDefault();

            if (user.Password != searchedUser.Password)
                throw new UserException("Contraseña incorrecta");

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

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public bool DeleteUser(long userId)
        {
            if (UnitOfWork.UserRepository.GetById(userId) != null)
            {
                UnitOfWork.UserRepository.Delete(userId);
                UnitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
