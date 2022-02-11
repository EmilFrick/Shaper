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
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public bool CheckedOut { get; set; }
        public DateTime OrderStarted { get; set; } = DateTime.Now;
        [Column(TypeName ="money")]
        public double OrderValue { get; set; } = 0;

        //Navigation Props
        [Required]
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        [ValidateNever]
        public ShaperUser Customer { get; set; }
        [ValidateNever]
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
