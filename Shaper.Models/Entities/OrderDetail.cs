using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        [Column(TypeName = "money")]
        public double UnitPrice { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
