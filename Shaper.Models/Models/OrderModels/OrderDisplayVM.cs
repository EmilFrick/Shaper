using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.Models.Models.OrderModels
{
    public class OrderDisplayVM
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public DateTime OrderPlaced { get; set; }
        [Required]
        public double OrderValue { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [ValidateNever]
        public ICollection<OrderDetail> OrderProducts { get; set; }

        public OrderDisplayVM()
        {

        }

        public OrderDisplayVM(Order order)
        {
            Id = order.Id;
            OrderPlaced = order.OrderPlaced;
            OrderValue = order.OrderValue;
            OrderProducts = order.OrderProducts;
        }

        public static IEnumerable<OrderDisplayVM> GetOrderDisplayVMs(IEnumerable<Order> orders)
        {
            List<OrderDisplayVM> orderDisplayVMs = new();
            foreach (var order in orders)
            {
                OrderDisplayVM vm = new OrderDisplayVM(order);
                orderDisplayVMs.Add(vm);
            }
            return orderDisplayVMs;
        }
    }
}
