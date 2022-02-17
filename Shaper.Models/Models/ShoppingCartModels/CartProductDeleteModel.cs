using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class CartProductDeleteModel
    {
        public string ProductName { get; set; }

        public string ShaperCustomer { get; set; }
    }
}
