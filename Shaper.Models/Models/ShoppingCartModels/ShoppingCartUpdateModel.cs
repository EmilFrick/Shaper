using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.ShoppingCartModels
{
    public class ShoppingCartUpdateModel
    {
        public bool? CheckedOut { get; set; }
        public string? UserIdentity { get; set; }
        public ICollection<ShoppingCartDeleteItemModel>? CartProducts { get; set; }
    }
}
