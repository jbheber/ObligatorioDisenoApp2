using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class User: ISoftDelete
    {
        /// <summary>
        /// Identifies the User.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User's name can be the email.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// If true, the user is administrator.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Unique code for user registration
        /// </summary>
        public Guid InvitationCode { get; set; }
    }
}
