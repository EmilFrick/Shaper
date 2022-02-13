using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ShoppingCartVM
{
    public class CartProductDeleteModel
    {
        public int ProductId { get; set; }

        public string ShaperCustomer { get; set; }
    }
}
