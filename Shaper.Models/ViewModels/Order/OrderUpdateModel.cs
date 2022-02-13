using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.ViewModels.Order
{
    public class OrderUpdateModel
    {
        public int OrderId { get; set; }
        public string? CustomerIdentity { get; set; }
        public List<OrderDetailModel>? OrderEntries { get; set; }
    }
}
