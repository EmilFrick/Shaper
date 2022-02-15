using Newtonsoft.Json;
using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;
using System.Net.Http.Headers;
using System.Text;

namespace Shaper.Web.ApiService.IService
{
    public class OrderApiService : ApiService<Order>, IOrderApiService
    {
        private readonly IHttpClientFactory _httpClient;

        public OrderApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Order> CheckoutUserShoppingCartAsync(string user, string url, string token)
        {
            if (user != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(JsonConvert.SerializeObject(new OrdersRequestModel() { Identity = user }), Encoding.UTF8, "application/json");
                if (token != null && token.Length != 0)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var res = await client.SendAsync(req);
                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Order>(jsonString);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Order>> OrdersByUserAsync(string user, string url, string token)
        {
            if (user != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(JsonConvert.SerializeObject(new OrdersRequestModel() { Identity = user }), Encoding.UTF8, "application/json");
                if (token != null && token.Length != 0)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var res = await client.SendAsync(req);
                if (res.IsSuccessStatusCode)
                {
                    var jsonString = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonString);
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