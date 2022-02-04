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
    public class ProductDisplayVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,500)]
        public double Price { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public DateTime Created { get; set; }


        [Required]
        public int ShapeId { get; set; }
        [ValidateNever]
        public Shape Shape { get; set; }
        [Required]
        public int ColorId { get; set; }
        [ValidateNever]
        public Color Color { get; set; }
        [Required]
        public int TransparencyId { get; set; }
        [ValidateNever]
        public Transparency Transparency { get; set; }
    }
}
