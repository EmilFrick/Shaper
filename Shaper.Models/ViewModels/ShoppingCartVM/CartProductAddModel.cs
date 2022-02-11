using Shaper.Models.ViewModels.ShaperUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.ShoppingCartVM
{
    public class CartProductAddModel
    {
        public int ProductId { get; set; }
        [Range(1,100)]
        public int ProductQuantity { get; set; }
        public ShaperUserDetails ShaperUserDetails{ get; set; }
    }
}
