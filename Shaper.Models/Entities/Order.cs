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
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderPlaced { get; set; }
        [Required]
        [Column(TypeName ="money")]
        public double OrderValue { get; set; }

        //Navigation Props
        [Required]
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        [ValidateNever]
        public ShaperUser Customer { get; set; }
        [ValidateNever]
        public ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
