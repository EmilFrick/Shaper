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
        public DateTime OrderStarted { get; set; }
        [Column(TypeName ="money")]
        public decimal OrderValue { get; set; }

        //Navigation Props
        [Required]
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        [ValidateNever]
        public ShaperUser Customer { get; set; }
        [ValidateNever]
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
