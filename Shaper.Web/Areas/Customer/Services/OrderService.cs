using Newtonsoft.Json;
using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Customer.Services.IServices;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using static Shaper.Utility.ApiPaths;

namespace Shaper.Web.Areas.Customer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClient;

        public OrderService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUserAsync(string user, string token)
        {
            if (user != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, ApiPath.Orders.GetEndpoint());
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

        public async Task<Order> GetOrderById(OrdersRequestModel ordersRequestModel, string token)
        {
            if (ordersRequestModel != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Get, ApiPath.Orders.GetEndpoint(ordersRequestModel.OrderId));
                req.Content = new StringContent(JsonConvert.SerializeObject(ordersRequestModel), Encoding.UTF8, "application/json");
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
    }
}

