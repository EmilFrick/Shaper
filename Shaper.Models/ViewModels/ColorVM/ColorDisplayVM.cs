﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using Shaper.Utility.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ColorVM
{
    public class ColorDisplayVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [HexValidation]
        public string Hex { get; set; }
        [Required]
        [Range(1,100)]

        public double AddedValue { get; set; }
        [ValidateNever]
        public ICollection<Product> Products { get; set; }

    }
}
