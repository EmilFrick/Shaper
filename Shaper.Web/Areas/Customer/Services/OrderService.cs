using Shaper.Models.Entities;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Customer.Services.IServices;
using static Shaper.Utility.ApiPaths;

namespace Shaper.Web.Areas.Customer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IShaperApiService _apiService;

        public OrderService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUserAsync(string user, string token)
        {
            return await _apiService.OrderApi.OrdersByUserAsync(user, ApiPath.Orders.GetEndpoint(), token);
        }

    }
}

