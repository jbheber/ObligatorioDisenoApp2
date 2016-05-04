using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Logic.API
{
    public interface IUserLogic
    {
        bool EmailIsUnique(string email);
        bool MailIsEmpty(string email);
        bool ValidPasswordLenght(string password);
        bool IsAlphaNumeric(string word);
        bool ValidInvitationCodeLenght(string invitationCode);
        void ValidateUser(User user, InvitationCode invitationCode);
        void RegisterUser(User user, InvitationCode invitationCode);
        void IsInDb(User user);
        User LogIn(User user);

    }
}
