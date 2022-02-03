using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ColorVM
{
    public class ColorCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Hex { get; set; }
        [Required]
        public double AddedValue { get; set; }
    }
}
