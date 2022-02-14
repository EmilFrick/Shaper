using Shaper.Models.Entities;

namespace Shaper.Web.ApiService.IService
{
    public class OrderApiService : ApiService<Order>, IOrderApiService
    {
        private readonly IHttpClientFactory _httpClient;

        public OrderApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }
    }
}