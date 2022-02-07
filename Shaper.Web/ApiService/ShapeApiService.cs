using Shaper.Models.Entities;
using Shaper.Web.ApiService.IService;

namespace Shaper.Web.ApiService
{
    public class ShapeApiService : ApiService<Shape>, IShapeApiService
    {

        private readonly IHttpClientFactory _httpClient;

        public ShapeApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
