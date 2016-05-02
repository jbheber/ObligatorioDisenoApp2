using Stockapp.Data.Interfaces;
using System;

namespace Stockapp.Data
{
    public class InvitationCode: ISoftDelete
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public Guid ParentUserId { get; set; }

        public virtual User ParentUser { get; set; }

        public bool IsDeleted { get; set; }
    }
}
