using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;

namespace Shaper.Web.Areas.Customer.Services.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersByUserAsync(string user, string token);
        Task<Order> GetOrderById(OrdersRequestModel ordersRequestModel, string token);
    }
}
