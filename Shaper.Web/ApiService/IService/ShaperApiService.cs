namespace Shaper.Web.ApiService.IService
{
    public class ShaperApiService : IShaperApiService
    {
        public IColorApiService ColorApi { get; private set; }

        private readonly IHttpClientFactory _httpClient;

        public ShaperApiService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            ColorApi = new ColorApiService(httpClient);
        }
    }
}
