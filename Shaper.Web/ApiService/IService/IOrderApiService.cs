using Shaper.Models.Entities;

namespace Shaper.Web.ApiService.IService
{
    public interface IOrderApiService : IApiService<Order>
    {
        Task<IEnumerable<Order>> OrdersByUserAsync(string user, string url, string token);
    }
}