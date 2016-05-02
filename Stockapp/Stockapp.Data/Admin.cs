using Stockapp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stockapp.Data
{
    public class Admin : ISoftDelete
    {
        /// <summary>
        /// DataBase generated Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Unique C.I. Alternate key
        /// </summary>
        public int CI { get; set; }

        /// <summary>
        /// Reference for the User
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Asociated User.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Individual name
        /// </summary>
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// Individual surname
        /// </summary>
        [StringLength(30)]
        public string Surname { get; set; }

        /// <summary>
        /// User email. Used for registration
        /// </summary>
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
