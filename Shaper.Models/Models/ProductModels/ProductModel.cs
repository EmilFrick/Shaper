using Newtonsoft.Json;
using Shaper.Models.Entities;
using Shaper.Models.Models.ColorModels;
using Shaper.Models.Models.ShapeModels;
using Shaper.Models.Models.TransparencyModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ProductModels
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public DateTime Created { get; set; }
        public ColorComponentModel Color { get; set; }
        public ShapeComponentModel Shape { get; set; }
        public TransparencyComponentModel Transparency { get; set; }

        public ProductModel()
        {

        }

        public ProductModel(CartProduct product)
        {
            Name = product.Product.Name;
            Artist = product.Product.Artist;
            Created = product.Product.Created;
            Color = new ColorComponentModel(product.Product.Color);
            Shape = new ShapeComponentModel(product.Product.Shape);
            Transparency = new TransparencyComponentModel(product.Product.Transparency);
        }
    }
}
