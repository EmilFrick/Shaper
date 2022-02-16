using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
                    ProductName = item.Product.Name,
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

        public async Task<Order> GetOrder(int orderId)
        {
            return await _db.Orders.GetFirstOrDefaultAsync(a => a.Id == orderId, includeProperties: "OrderProducts");
        }

        public async Task UpdateOrder(OrderUpdateModel updateOrder, Order originalOrder)
        {
            originalOrder.CustomerIdentity = updateOrder.CustomerIdentity is not null ? updateOrder.CustomerIdentity : originalOrder.CustomerIdentity;
            if (updateOrder.OrderEntries is not null)
                UpdateOrderDetailsCollection(updateOrder.OrderEntries, originalOrder.OrderProducts.ToList());

            _db.Orders.Update(originalOrder);
            await _db.SaveAsync();
        }

        private void UpdateOrderDetailsCollection(List<OrderDetailModel> updatedOrderEntries, List<OrderDetail> originalOrderEntries)
        {
            foreach (var updatedOrderEntry in updatedOrderEntries)
            {
                foreach (var orderDetail in originalOrderEntries)
                {
                    if (updatedOrderEntry.Id == orderDetail.Id)
                    {

                        if (updatedOrderEntry?.ProductId is not null)
                            orderDetail.ProductId = updatedOrderEntry.ProductId.GetValueOrDefault();

                        if (updatedOrderEntry?.ProductName is not null)
                            orderDetail.ProductName = updatedOrderEntry.ProductName;

                        if (updatedOrderEntry?.ColorName is not null)
                            orderDetail.ColorName = updatedOrderEntry.ColorName;

                        if (updatedOrderEntry?.ColorHex is not null)
                            orderDetail.ColorHex = updatedOrderEntry.ColorHex;

                        if (updatedOrderEntry?.ShapeName is not null)
                            orderDetail.ShapeName = updatedOrderEntry.ShapeName;

                        if (updatedOrderEntry?.ShapeHasFrame is not null)
                            orderDetail.ShapeHasFrame = updatedOrderEntry.ShapeHasFrame.GetValueOrDefault();

                        if (updatedOrderEntry?.TransparencyName is not null)
                            orderDetail.TransparencyName = updatedOrderEntry.TransparencyName;

                        if (updatedOrderEntry?.TransparencyDescription is not null)
                            orderDetail.TransparencyDescription = updatedOrderEntry.TransparencyDescription;

                        if (updatedOrderEntry?.TransparencyValue is not null)
                            orderDetail.TransparencyValue = updatedOrderEntry.TransparencyValue.GetValueOrDefault();

                        if (updatedOrderEntry?.ProductQuantity is not null)
                            orderDetail.ProductQuantity = updatedOrderEntry.ProductQuantity.GetValueOrDefault();

                        if (updatedOrderEntry?.ProductUnitPrice is not null)
                            orderDetail.ProductUnitPrice = updatedOrderEntry.ProductUnitPrice.GetValueOrDefault();

                        if (updatedOrderEntry?.ProductUnitPrice is not null || updatedOrderEntry?.ProductQuantity is not null)
                            orderDetail.EntryTotalValue = (orderDetail.ProductUnitPrice * orderDetail.ProductQuantity);
                    }
                }
            }
        }

        public async Task DeleteOrder(Order order)
        {
            _db.Orders.Remove(order);
            await _db.SaveAsync();
        }
    }
}
