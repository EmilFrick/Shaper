using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ProductVM
{
    public class ProductCreateVM
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;


        [Required]
        public int ShapeId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public int TransparencyId { get; set; }
    }
}
