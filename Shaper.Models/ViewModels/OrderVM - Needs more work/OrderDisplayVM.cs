﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.OrderVM
{
    public class OrderDisplayVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime OrderPlaced { get; set; }
        [Required]
        public decimal OrderValue { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [ValidateNever]
        public ICollection<OrderDetail> OrderProducts { get; set; }

    }
}
