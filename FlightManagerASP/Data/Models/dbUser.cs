using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Index(nameof(dbUser.EGN), IsUnique = true)]
    public class dbUser : IdentityUser<string>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Username must be longer than 5 symbols.", MinimumLength = 6)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Password must be longer than 4 symbols.", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "First name must be longer than 2 letters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain only letters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Last name must be longer than 2 letters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name must contain only letters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "EGN must be exactly 10 digits long.", MinimumLength = 10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "EGN must contain only digits.")]
        [Display(Name = "EGN")]
        public string EGN { get; set; }
        [Required]
        [Display(Name = "Address")]
        [StringLength(60, ErrorMessage = "Address must contain more than 10 characters.", MinimumLength = 11)]
        public string Address { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Phone number must be exactly 10 digits long.", MinimumLength = 10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must contain only digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        //[RegularExpression("^(Admin|Employee)$", ErrorMessage = "Role must be either admin or employee. (If admin exists, there can not be another one)")]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
