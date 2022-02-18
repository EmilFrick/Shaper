using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class ShoppingCartDeleteItemModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
