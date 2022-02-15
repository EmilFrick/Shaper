using Shaper.Models.Entities;

namespace Shaper.Web.ApiService.IService
{
    public interface IOrderApiService : IApiService<Order>
    {
        Task<Order> CheckoutUserShoppingCartAsync(string user, string url, string token);
        Task<IEnumerable<Order>> OrdersByUserAsync(string user, string url, string token);
    }
}