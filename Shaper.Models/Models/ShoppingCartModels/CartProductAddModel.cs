using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class CartProductAddModel
    {
        public int ProductId { get; set; }
        [Range(1,100)]
        public int ProductQuantity { get; set; }
        public string ShaperCustomer { get; set; }
    }
}
