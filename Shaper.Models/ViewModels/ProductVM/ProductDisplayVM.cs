using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ProductVM
{
    public class ProductDisplayVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Artist { get; set; }
        public DateTime Created { get; set; }
        public int ShapeId { get; set; }
        public Shape Shape { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int TransparencyId { get; set; }
        public Transparency Transparency { get; set; }

        public ProductDisplayVM()
        {

        }
        
        public ProductDisplayVM(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Artist = product.Artist;
            Created = product.Created;
            ColorId = product.ColorId;
            Color = product.Color;
            ShapeId = product.ShapeId;
            Shape = product.Shape;
            TransparencyId = product.TransparencyId;
            Transparency = product.Transparency;
        }

        public static IEnumerable<ProductDisplayVM> GetProductDisplayVMs(IEnumerable<Product> products)
        {
            List<ProductDisplayVM> productDisplayVMs = new List<ProductDisplayVM>();
            foreach (Product product in products)
            {
                ProductDisplayVM productDisplayVM = new ProductDisplayVM(product);
                productDisplayVMs.Add(productDisplayVM);
            }
            return productDisplayVMs;
        }
    }
}
