using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class UserShoppingCartModel
    {
        public double OrderValue { get; set; }
        public int ArticleCount { get; set; }
        public ICollection<CartProductModel> CartProducts { get; set; } = new List<CartProductModel>();

        public UserShoppingCartModel()
        {

        }
        public UserShoppingCartModel(ShoppingCart shoppingCart)
        {
            foreach (var product in shoppingCart.CartProducts)
            {
                CartProductModel productModel = new CartProductModel(product);
                OrderValue += productModel.ProductQuantity * productModel.UnitPrice;
                ArticleCount++;
                CartProducts.Add(productModel);
            }
        }
    }
}
