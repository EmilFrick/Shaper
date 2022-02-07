using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaper.Utility.CustomValidations;

namespace Shaper.Models.ViewModels.ShapeVM
{
    public class ShapeUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [ShapeValidation]
        [Display(Name = "Shape")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Frame")]
        public bool HasFrame { get; set; }
        [Required]
        [Display(Name = "Shape Value")]
        [Range(1, 100)]
        public double AddedValue { get; set; }

        public ShapeUpdateVM()
        {

        }

        public ShapeUpdateVM(Shape shape)
        {
            Id = shape.Id;
            Name = shape.Name;
            HasFrame = shape.HasFrame;
            AddedValue = shape.AddedValue;
        }

        public Shape GetShapeFromUpdateVM()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name,
                HasFrame = this.HasFrame,
                AddedValue = this.AddedValue,
            };
        }
    }
}
