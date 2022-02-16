using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shaper.Models.Entities;
using Shaper.Models.Models.ProductComponentsModels;
using Shaper.Models.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ProductModels
{
    public class ProductUpsertModel
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
        [DisplayName("Color")]
        public int ColorId { get; set; }
        [Required]
        [DisplayName("Shape")]
        public int ShapeId { get; set; }
        [Required]
        [DisplayName("Transparency")]
        public int TransparencyId { get; set; }



        [ValidateNever]
        public List<SelectListItem> Colors { get; set; }
        [ValidateNever]
        public List<SelectListItem> Shapes { get; set; }
        [ValidateNever]
        public List<SelectListItem> Transparencies { get; set; }

        public ProductUpsertModel()
        {

        }

        public ProductUpsertModel(Product product)
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
        }

        public ProductUpsertModel(IEnumerable<Color> colors,
            IEnumerable<Shape> shapes, IEnumerable<Transparency> transparencies)
        {
            Colors = colors.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Shapes = shapes.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Transparencies = transparencies.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();
        }

        public ProductUpsertModel(Product product, IEnumerable<Color> colors,
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

            Colors = colors.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Shapes = shapes.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Transparencies = transparencies.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();
        }

        public Product VmToNewProduct(ProductResComponentsModel components)
        {
            return new()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Artist = this.Artist,
                Price = components.ColorComponent.AddedValue + components.TransparencyComponent.AddedValue + components.ShapeComponent.AddedValue,
                Created = this.Created,
                ColorId = components.ColorComponent.Id,
                ShapeId = components.ShapeComponent.Id,
                TransparencyId = components.TransparencyComponent.Id,
            };
        }
    }
}
