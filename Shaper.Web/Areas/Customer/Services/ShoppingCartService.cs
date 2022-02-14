using Shaper.Web.ApiService.IService;

namespace Shaper.Web.Areas.Customer.Services
{
    public class ShoppingCartService
    {
        private readonly IShaperApiService _apiService;

        public ShoppingCartService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }
    }
}
