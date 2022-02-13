using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers
{
    public interface IOrderHandler
    {
        Task<Order> InitateOrderAsync(string user);
        Task CheckOutCartProducts(ShoppingCart cart, Order order);
        Task ReconciliatingOrder(int id);
        Task<IEnumerable<Order>> GetUserOrders(string user);
    }
}