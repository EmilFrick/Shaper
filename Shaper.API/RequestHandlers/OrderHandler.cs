using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using System.Collections.Generic;

namespace Shaper.API.RequestHandlers
{
    public class OrderHandler : IOrderHandler
    {

        private readonly IUnitOfWork _db;

        public OrderHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<Order> InitateOrderAsync(string user)
        {
            Order order = new() { CustomerIdentity = user };
            await _db.Orders.AddAsync(order);
            await _db.SaveAsync();
            return order;
        }

        public async Task CheckOutCartProducts(ShoppingCart cart, Order order)
        {
            double orderTotalValue = 0;
            List<OrderDetail> orderDetails = new();
            foreach (var item in cart.CartProducts)
            {
                OrderDetail detail = new()
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ColorName = item.Product.Color.Name,
                    ColorHex = item.Product.Color.Hex,

                    ShapeName = item.Product.Shape.Name,
                    ShapeHasFrame = item.Product.Shape.HasFrame,

                    TransparencyName = item.Product.Transparency.Name,
                    TransparencyDescription = item.Product.Transparency.Description,
                    TransparencyValue = item.Product.Transparency.Value,

                    ProductQuantity = item.ProductQuantity,
                    ProductUnitPrice = item.UnitPrice,
                    EntryTotalValue = (item.ProductQuantity * item.UnitPrice),
                };
                orderDetails.Add(detail);
            }
            await _db.OrderDetails.AddRangeAsync(orderDetails);
            await _db.SaveAsync();
        }

        public async Task ReconciliatingOrder(int orderId)
        {
            double orderTotalValue = 0;
            var order = await _db.Orders.GetFirstOrDefaultAsync(a => a.Id == orderId, includeProperties: "OrderProducts");
            foreach (var item in order.OrderProducts)
            {
                orderTotalValue += item.EntryTotalValue;
            }
            order.OrderValue = orderTotalValue;
            _db.Orders.Update(order);
            await _db.SaveAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string user)
        {
            return await _db.Orders.GetUserOrders(user);
        }
    }
}
