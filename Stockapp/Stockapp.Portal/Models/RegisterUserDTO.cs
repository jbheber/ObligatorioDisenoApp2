using Stockapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stockapp.Portal.Models
{
    public class RegisterUserDTO
    {
        public User User { get; set; }

        public InvitationCode InvitationCode { get; set; }
    }
}