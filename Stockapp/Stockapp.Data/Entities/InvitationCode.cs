using Stockapp.Data.Interfaces;
using System;

namespace Stockapp.Data
{
    public class InvitationCode : ISoftDelete, Identificable
    {
        /// <summary>
        /// Database generated Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Generated Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Reference to that created the code
        /// </summary>
        public Guid ParentUserId { get; set; }

        /// <summary>
        /// User that created the code
        /// </summary>
        public virtual User ParentUser { get; set; }

        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        public InvitationCode()
        {
            IsDeleted = false;
        }

        public InvitationCode(User user)
        {
            IsDeleted = false;
            ParentUser = user;
        }

    }
}
