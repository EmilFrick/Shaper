using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers
{
    public interface IOrderHandler
    {
        Task<Order> InitateOrderAsync(ShaperUser user);
        Task CheckOutCartProducts(ShoppingCart cart, Order order);
    }
}