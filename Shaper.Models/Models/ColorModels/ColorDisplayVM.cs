using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using Shaper.Utility.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ColorModels
{
    public class ColorDisplayVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hex { get; set; }
        public double AddedValue { get; set; }

        public ColorDisplayVM(Color color)
        {
            Id = color.Id;
            Name = color.Name;
            Hex = color.Hex;
            AddedValue = color.AddedValue;
        }

        public static IEnumerable<ColorDisplayVM> ColorDisplayVMs(IEnumerable<Color> colors)
        {
            List<ColorDisplayVM> colorDisplayVMs = new List<ColorDisplayVM>();
            foreach (var color in colors)
            {
                var colorVM = new ColorDisplayVM(color);
                colorDisplayVMs.Add(colorVM);
            }
            return colorDisplayVMs;
        }
    }
}
