using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.OrderModels
{
    public class OrderDetailModel
    {
        [Required]
        public int Id { get; set; }

        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        
        //Color
        public string? ColorName { get; set; }
        public string? ColorHex { get; set; }


        //Shape
        public string? ShapeName { get; set; }
        public bool? ShapeHasFrame { get; set; }


        //Transparency
        public string? TransparencyName { get; set; }
        public string? TransparencyDescription { get; set; }
        [Range(0,100)]
        public int? TransparencyValue { get; set; }

        //OrderFinanace
        [Range(1,100)]
        public int? ProductQuantity { get; set; }
        public double? ProductUnitPrice { get; set; }
        public double? EntryTotalValue { get; set; }
    }
}
