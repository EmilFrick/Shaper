using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ColorModels
{
    public class ColorComponentModel
    {
        public string Name { get; set; }
        public string Hex { get; set; }


        public ColorComponentModel()
        {

        }
        public ColorComponentModel(Color color)
        {
            Name = color.Name;
            Hex = color.Hex;
        }
    }
}
