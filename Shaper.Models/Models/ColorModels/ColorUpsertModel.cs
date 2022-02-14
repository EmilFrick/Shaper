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
    public class ColorUpsertModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [HexValidation]
        public string Hex { get; set; }
        [Required]
        [Range(1, 100)]
        public double AddedValue { get; set; }

        public ColorUpsertModel()
        {

        }
        public ColorUpsertModel(Color color)
        {
            Id = color.Id;
            Name = color.Name;
            Hex = color.Hex;
            AddedValue = color.AddedValue;
        }

        public Color GetColorFromUpdateVM()
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name,
                Hex = this.Hex.ToUpper(),
                AddedValue = this.AddedValue
            };
        }
    }
}
