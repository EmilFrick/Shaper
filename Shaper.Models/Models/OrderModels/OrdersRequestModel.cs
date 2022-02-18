using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.OrderModels
{
    public class OrdersRequestModel
    {
        public string Identity { get; set; }
        public int? OrderId { get; set; }
    }
}
