using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ShoppingCartVM
{
    public class CartProductAddModel
    {
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string CustomerIdentity { get; set; }
    }
}
