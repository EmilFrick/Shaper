namespace Shaper.Web.ApiService.IService
{
    public class ShaperApiService : IShaperApiService
    {
        public IColorApiService ColorApi { get; private set; }
        public IShapeApiService ShapeApi { get; private set; }
        public ITransparencyApiService TransparencyApi { get; private set; }
        public IProductApiService ProductApi { get; private set; }

        

        private readonly IHttpClientFactory _httpClient;

        public ShaperApiService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            ColorApi = new ColorApiService(httpClient);
            ShapeApi = new ShapeApiService(httpClient);
            TransparencyApi = new TransparencyApiService(httpClient);
            ProductApi = new ProductApiService(httpClient);
        }
    }
}
