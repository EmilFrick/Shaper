using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.OrderModels
{
    public class OrderEntryVM
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderValue { get; set; }
        public int AmountOfArticles { get; set; }

        public OrderEntryVM()
        {

        }

        public OrderEntryVM(Order order)
        {
            Id = order.Id;
            OrderDate = order.OrderPlaced;
            OrderValue = order.OrderValue;
            AmountOfArticles = order.OrderProducts.Count;
        }
    }
}
