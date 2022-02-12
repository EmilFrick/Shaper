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

        public async Task<Order> InitateOrderAsync(ShaperUser user)
        {
            Order order = new() { Id = user.Id };
            await _db.Orders.AddAsync(order);
            await _db.SaveAsync();
            return order;
        }

        public async Task CheckOutCartProducts(ShoppingCart cart, Order order)
        {
            List<OrderDetail> productDetails = new();

            foreach (var item in cart.CartProducts)
            {
                OrderDetail detail = new()
                {
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
                productDetails.Add(detail);
            }
            await _db.OrderDetails.AddRangeAsync(productDetails);
            await _db.SaveAsync();
        }
    }
}
