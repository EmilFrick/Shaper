using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ProductComponentsVM
{
    public class ProductResComponentsVM
    {
        public Color ColorComponent { get; set; }
        public Shape ShapeComponent { get; set; }
        public Transparency TransparencyComponent { get; set; }
    }
}
