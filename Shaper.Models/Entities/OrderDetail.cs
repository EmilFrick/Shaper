using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Shaper.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        //Created to keep tabs on how many of a certain products has been sold. Can monitor to see when it was doing best. Useful for the Artist.
        public int ProductId { get; set; }

        //Color
        public string ColorName { get; set; }
        public string ColorHex { get; set; }

        //Shape
        public string ShapeName { get; set; }
        public bool ShapeHasFrame { get; set; }

        //Transparency
        public string TransparencyName { get; set; }
        public string? TransparencyDescription { get; set; }
        public int TransparencyValue { get; set; }

        //OrderFinanace
        public int ProductQuantity { get; set; }
        [Column(TypeName = "money")]
        public double ProductUnitPrice { get; set; }
        [Column(TypeName = "money")]
        public double EntryTotalValue { get; set; }


        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ValidateNever]
        public Order Order { get; set; }
    }
}
