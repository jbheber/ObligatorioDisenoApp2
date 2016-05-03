using Stockapp.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stockapp.Data
{
    public class User : ISoftDelete, Identificable
    {
        /// <summary>
        /// Identifies the User.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// User's name can be the email.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// User email. Used for registration
        /// </summary>
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// If true, the user is administrator.
        /// </summary>
        public bool IsAdmin { get; set; }


        /// <summary>
        /// Soft delete
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
