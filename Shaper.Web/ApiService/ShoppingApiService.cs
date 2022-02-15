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

        public async Task<ShoppingCart> GetUserShoppingCart(string user, string url, string token = "")
        {
            if (user is not null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(JsonConvert.SerializeObject(new ShoppingCartRequestModel() { Identity= user}), Encoding.UTF8, "application/json");
                if (token != null && token.Length != 0)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var res = await client.SendAsync(req);
                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ShoppingCart>(jsonString);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}