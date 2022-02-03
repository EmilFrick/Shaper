﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ShapeVM
{
    public class ShapeCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,100)]
        public double AddedValue { get; set; }


        public Shape GetShapeFromCreateVM()
        {
            return new()
            {
                Name = this.Name,
                AddedValue = this.AddedValue,
            };
        }
    }
}
