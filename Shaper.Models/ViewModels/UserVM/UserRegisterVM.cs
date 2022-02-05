using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Shaper.Utility.SD;

namespace Shaper.Models.ViewModels.UserVM
{
    public class UserRegisterVM
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        public UserType UserType{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password does not match.")]
        public string ConfirmPassword { get; set; }

        public ApplicationUser VMtoUser()
        {
            return new()
            {
                UserName = this.Email,
                FullName = Fullname,
                Email = this.Email,
                Address = this.Address,
                City = this.City,
                PostalCode = this.PostalCode,
                Role = this.UserType.ToString()
            };
        }
    }
}
