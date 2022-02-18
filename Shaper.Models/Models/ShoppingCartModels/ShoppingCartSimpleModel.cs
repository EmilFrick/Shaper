using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class ShoppingCartSimpleModel
    {
        public string UserIdentity { get; set; }
        public DateTime ShoppingCartOpened { get; set; }
        public double OrderValue { get; set; }
        public bool CheckedOut { get; set; }
        public int DistinctItemsCount { get; set; }
        public ShoppingCartSimpleModel()
        {

        }

        public ShoppingCartSimpleModel(ShoppingCart cart)
        {
            UserIdentity = cart.CustomerIdentity;
            ShoppingCartOpened = cart.OrderStarted;
            OrderValue = cart.OrderValue;
            CheckedOut = cart.CheckedOut;
            if(cart.CartProducts is not null)
                DistinctItemsCount = cart.CartProducts.Count;
        }
    }
}
