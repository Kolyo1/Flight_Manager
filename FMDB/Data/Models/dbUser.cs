using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Index(nameof(dbUser.EGN), IsUnique = true)]
    public class dbUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public string UserName { get; set; }
        [NotNull]
        [MaxLength(20)]
        public string Password { get; set; }
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [NotNull]
        [MaxLength(30)]
        public string LastName { get; set; }
        [NotNull]
        [MaxLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "EGN must contain only digits")]
        public string EGN { get; set; }
        [NotNull]
        public string Address { get; set; }
        [NotNull]
        [MaxLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; }
        public bool isAdmin { get; set; }

        public dbUser()
        {
            
        }
    }
}
