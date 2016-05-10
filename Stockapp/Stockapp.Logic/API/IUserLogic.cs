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
        /// <summary>
        /// Checks if the email isn't registred to another user.
        /// </summary>
        /// <param name="email">String with the email</param>
        /// <returns></returns>
        bool EmailIsUnique(string email);

        /// <summary>
        /// Checks the email isn't empty.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool MailIsEmpty(string email);

        /// <summary>
        /// Checks the typed password matches the game settings.
        /// </summary>
        /// <param name="password">For now string</param>
        /// <returns></returns>
        bool ValidPasswordLenght(string password);

        /// <summary>
        /// Checks the password is alphanumeric
        /// </summary>
        /// <param name="word">Password</param>
        /// <returns></returns>
        bool IsAlphaNumeric(string word);

        /// <summary>
        /// Checks the invitation code is in the database
        /// </summary>
        /// <param name="invitationCode"></param>
        /// <returns></returns>
        bool ValidInvitationCode(InvitationCode invitationCode);

        /// <summary>
        /// Checks the user is valid. If it isn't valid then throw user exception.
        /// </summary>
        /// <param name="user">User to be created</param>
        /// <param name="invitationCode">Invitation code send</param>
        void ValidateUser(User user, InvitationCode invitationCode);

        /// <summary>
        /// Try to register a new user.
        /// </summary>
        /// <param name="user">New user</param>
        /// <param name="invitationCode">Invitation code the user recieved</param>
        bool RegisterUser(User user, InvitationCode invitationCode);

        /// <summary>
        /// Checks if that user is already registered in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsInDb(User user);

        /// <summary>
        /// Finds the user and returns it.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User LogIn(User user);

        /// <summary>
        /// Gets all users from database.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">Modified user</param>
        /// <returns></returns>
        bool UpdateUser(User user);

        bool DeleteUser(Guid userId);

        void Dispose();
    }
}
