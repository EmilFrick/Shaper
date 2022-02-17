using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShapeModels
{
    public  class ShapeComponentModel
    {
        public string Name { get; set; }
        public bool HasFrame { get; set; }

        public ShapeComponentModel()
        {

        }
        public ShapeComponentModel(Shape shape)
        {
            Name = shape.Name.ToLower();
            HasFrame = shape.HasFrame;
        }
    }
}
