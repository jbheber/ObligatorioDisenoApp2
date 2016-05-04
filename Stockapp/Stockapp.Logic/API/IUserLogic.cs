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
        bool UniqueUserEmail(string email);
        bool EmptyMail(string email);
        bool PasswordLenght(string password);
        bool AlphaNumeric(string word);
        bool InvitationCodeLenght(string invitationCode);
        void RegisterValidations(User user, InvitationCode invitationCode);
        void Register(User user, InvitationCode invitationCode);
        void IsInDb(User user);
        User LogIn(User user);

    }
}
