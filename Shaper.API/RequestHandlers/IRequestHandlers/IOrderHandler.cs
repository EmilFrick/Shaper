using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;

namespace Shaper.API.RequestHandlers
{
    public interface IOrderHandler
    {
        Task<Order> InitateOrderAsync(string user);
        Task CheckOutCartProducts(ShoppingCart cart, Order order);
        Task ReconciliatingOrder(int id);
        Task<IEnumerable<Order>> GetUserOrders(string user);
        Task<Order> GetOrder(int orderId);
        Task UpdateOrder(OrderUpdateModel order, Order originalOrder);
        Task DeleteOrder(Order order);
    }
}