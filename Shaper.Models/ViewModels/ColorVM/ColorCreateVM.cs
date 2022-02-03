using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using Shaper.Utility.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Shaper.Models.Entities.Color;

namespace Shaper.Models.ViewModels.ColorVM
{
    public class ColorCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [HexValidation]
        public string Hex { get; set; }
        [Required]
        [Range(1,100)]
        public double AddedValue { get; set; }

        public Color GetColorFromCreateVM()
        {
            return new()
            {
                Name = this.Name,
                Hex = this.Hex.ToUpper(),
                AddedValue = this.AddedValue
            };
        }

    }
}
