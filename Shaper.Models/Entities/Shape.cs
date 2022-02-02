using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Entities
{
    public class Shape
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //Navigation
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}
