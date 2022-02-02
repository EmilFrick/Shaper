using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        //Navigation Props
        [ValidateNever]
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
        [ValidateNever]
        public ICollection<Order> Orders { get; set; }

    }
}
