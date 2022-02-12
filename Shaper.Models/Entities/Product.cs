using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,500)]
        [Column(TypeName ="money")]
        public double Price { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public DateTime Created { get; set; }

        //Navigation Props
        [Required]
        [ForeignKey("ShapeId")]
        public int ShapeId { get; set; }
        [ValidateNever]
        public Shape Shape { get; set; }

        [Required]
        [ForeignKey("ColorId")]
        public int ColorId { get; set; }
        [ValidateNever]
        public Color Color { get; set; }

        [Required]
        [ForeignKey("TransparencyId")]
        public int TransparencyId { get; set; }
        [ValidateNever]
        public Transparency Transparency { get; set; }


        [ValidateNever]
        public ICollection<CartProduct> CartProducts { get; set; }

    }
}
