using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ProductModels
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [ValidateNever]
        public string Artist { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public int ColorId { get; set; }
        [ValidateNever]
        public Color Color { get; set; }
        [Required]
        public int ShapeId { get; set; }
        [ValidateNever]
        public Shape Shape { get; set; }
        [Required]
        public int TransparencyId { get; set; }
        [ValidateNever]
        public Transparency Transparency { get; set; }

        public ProductDetailsModel()
        {

        }

        public ProductDetailsModel(Product product)
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

    }
}
