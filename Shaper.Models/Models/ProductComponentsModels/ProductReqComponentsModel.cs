using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ProductComponentsModels
{
    public class ProductReqComponentsModel
    {
        public int ColorId { get; set; }
        public int ShapeId { get; set; }
        public int TransparencyId { get; set; }

        public ProductReqComponentsModel(int color, int shape, int transparency)
        {
            ColorId = color;
            ShapeId = shape;
            TransparencyId = transparency;
        }
    }
}
