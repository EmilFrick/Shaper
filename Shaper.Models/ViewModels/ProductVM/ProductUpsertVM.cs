using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProductUpsertVM
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
        public double ColorCost { get; set; }
        [Required]
        public int ShapeId { get; set; }
        [ValidateNever]
        public double ShapeCost { get; set; }
        [Required]
        public int TransparencyId { get; set; }
        [ValidateNever]
        public double TransparencyCost { get; set; }



        [ValidateNever]
        public List<SelectListItem> Colors { get; set; }
        [ValidateNever]
        public List<SelectListItem> Shapes { get; set; }
        [ValidateNever]
        public List<SelectListItem> Transparencies { get; set; }

        public ProductUpsertVM()
        {

        }

        public ProductUpsertVM(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Artist = product.Artist;
            Created = product.Created;
            ColorId = product.ColorId;
            ColorCost = product.Color.AddedValue;
            ShapeId = product.ShapeId;
            ShapeCost = product.Shape.AddedValue;
            TransparencyId = product.TransparencyId;
            TransparencyCost = product.Transparency.AddedValue;
        }

        public ProductUpsertVM(IEnumerable<Color> colors,
            IEnumerable<Shape> shapes, IEnumerable<Transparency> transparencies)
        {
            Colors = colors.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Shapes = shapes.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Transparencies = transparencies.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();
        }

        public ProductUpsertVM(Product product, IEnumerable<Color> colors,
                                 IEnumerable<Shape> shapes, IEnumerable<Transparency> transparencies)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Artist = product.Artist;
            Created = product.Created;
            ColorId = product.ColorId;
            ShapeId = product.ShapeId;
            TransparencyId = product.TransparencyId;
            ShapeCost = product.Shape.AddedValue;
            TransparencyId = product.TransparencyId;
            TransparencyCost = product.Transparency.AddedValue;

            Colors = colors.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Shapes = shapes.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Transparencies = transparencies.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();
        }

        //public Product VmToEntity()
        //{
        //   return new()
        //   {
        //       Id = this.Id,
        //    Name = this.Name,
        //    Description = this.Description,
        //    Price = product.Price;
        //    Artist = product.Artist;
        //    Created = product.Created;
        //    ColorId = product.ColorId;
        //    Color = product.Color;
        //    ShapeId = product.ShapeId;
        //    Shape = product.Shape;
        //    TransparencyId = product.TransparencyId;
        //   } 
        //}
    }
}
