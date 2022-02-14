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

namespace Shaper.Models.Models.TransparencyModels
{
    public class TransparencyCreateModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public double AddedValue { get; set; }

            
        public Transparency GetTransparencyFromCreateVM()
        {
            return new()
            {
                Name = this.Name,
                Description = this.Description,
                Value = this.Value,
                AddedValue = this.AddedValue
            };
        }
    }
}
