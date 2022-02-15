using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class ShoppingCartDisplayVM
    {
        public int Id { get; set; }
        public double OrderValue { get; set; } = 0;
        public ICollection<CartProduct>? CartProducts { get; set; }


        public ShoppingCartDisplayVM()
        {

        }

        public ShoppingCartDisplayVM(ShoppingCart shoppingCart)
        {
            Id = shoppingCart.Id;
            OrderValue = shoppingCart.OrderValue;
            CartProducts = shoppingCart.CartProducts;
        }
    }
}
