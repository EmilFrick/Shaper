using Shaper.Models.Entities;
using Shaper.Web.ApiService.IService;

namespace Shaper.Web.ApiService
{
    public class TransparencyApiService : ApiService<Transparency>, ITransparencyApiService
    {

        private readonly IHttpClientFactory _httpClient;

        public TransparencyApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
