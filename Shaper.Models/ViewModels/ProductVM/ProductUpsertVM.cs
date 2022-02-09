using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductComponentsVM;
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
        [Required]
        public int ShapeId { get; set; }
        [Required]
        public int TransparencyId { get; set; }



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
            ShapeId = product.ShapeId;
            TransparencyId = product.TransparencyId;
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
            ShapeId = product.TransparencyId;
            TransparencyId = product.TransparencyId;

            Colors = colors.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Shapes = shapes.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();

            Transparencies = transparencies.Select(i => new SelectListItem
            { Text = i.Name, Value = i.Id.ToString() }).ToList();
        }

        public Product VmToNewProduct(ProductResComponentsVM components)
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
