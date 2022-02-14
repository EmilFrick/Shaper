using Newtonsoft.Json;
using Shaper.Models.Entities;
using Shaper.Models.Models.ShoppingCartModels;
using Shaper.Web.ApiService.IService;
using System.Net.Http.Headers;
using System.Text;

namespace Shaper.Web.ApiService
{
    public class ShoppingApiService : ApiService<ShoppingCart>, IShoppingCartApiService
    {
        
        private readonly IHttpClientFactory _httpClient;

        public ShoppingApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddingProductToUserShoppingCart(CartProductAddModel productToAdd, string url, string token = "")
        {
            if (productToAdd != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(JsonConvert.SerializeObject(productToAdd), Encoding.UTF8, "application/json");
                if (token != null && token.Length != 0)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var res = await client.SendAsync(req);
                if (res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}