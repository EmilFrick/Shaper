using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ColorVM
{
    public class ColorUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Hex { get; set; }
        [Required]
        public double AddedValue { get; set; }
    }
}
