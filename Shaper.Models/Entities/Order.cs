﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderPlaced { get; set; }
        [Required]
        public decimal OrderValue { get; set; }

        //Navigation Props
        [Required]
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        [ValidateNever]
        public ApplicationUser Customer { get; set; }
        [ValidateNever]
        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
