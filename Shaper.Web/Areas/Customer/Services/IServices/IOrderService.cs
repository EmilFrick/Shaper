using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Customer.Services.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersByUserAsync(string user, string token);
    }
}
