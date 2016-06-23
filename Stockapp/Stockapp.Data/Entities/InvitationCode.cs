using Stockapp.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockapp.Data
{
    public class InvitationCode : ISoftDelete, Identificable
    {
        /// <summary>
        /// Database generated Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Generated Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Reference to that created the code
        /// </summary>
        public long ParentUserId { get; set; }

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
