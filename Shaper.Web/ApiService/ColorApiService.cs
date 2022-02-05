using Shaper.Models.Entities;
using Shaper.Web.ApiService.IService;

namespace Shaper.Web.ApiService
{
    public class ColorApiService : ApiService<Color>, IColorApiService
    {

        private readonly IHttpClientFactory _httpClient;

        public ColorApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
