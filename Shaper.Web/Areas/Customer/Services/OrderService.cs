using Shaper.Web.ApiService.IService;

namespace Shaper.Web.Areas.Customer.Services
{
    public class OrderService
    {
        private readonly IShaperApiService _apiService;

        public OrderService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }
    }
}

