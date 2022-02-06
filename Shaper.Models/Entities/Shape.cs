using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Utility.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ShapeValidation]
        [Display(Name = "Shape")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Frame")]
        public bool HasFrame { get; set; }
        [Required]
        [Range(1, 100)]
        [Display(Name = "Shape Value")]
        [Column(TypeName = "money")]
        public double AddedValue { get; set; }

        //Navigation
        [ValidateNever]
        public ICollection<Product> Products { get; set; }
    }
}
