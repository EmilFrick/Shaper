using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaper.Models.Models.ProductModels;
using System.Xml.Linq;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class CartProductModel
    {
        public ProductModel Product { get; set; } = new();
        public int ProductQuantity { get; set; }
        public double UnitPrice { get; set; }

        public CartProductModel()
        {

        }

        public CartProductModel(CartProduct cartProduct)
        {
            Product = new ProductModel(cartProduct);
            ProductQuantity = cartProduct.ProductQuantity;
            UnitPrice = cartProduct.UnitPrice;
        }
    }
}
