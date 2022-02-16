using Newtonsoft.Json;
using Shaper.Models.Entities;
using Shaper.Models.Models.ShoppingCartModels;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Customer.Services.IServices;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using static Shaper.Utility.ApiPaths;
using Shaper.Models.Models.OrderModels;
using System;

namespace Shaper.Web.Areas.Customer.Services
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly IHttpClientFactory _httpClient;
        public ShoppingCartService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddProductToUsersCartAsync(int id, int quantity, string user, string token)
        {
            CartProductAddModel productToAdd = new()
            {
                ProductId = id,
                ProductQuantity = quantity,
                ShaperCustomer = user
            };

            if (productToAdd != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, ApiPath.ShoppingCartsAddItem.GetEndpoint());
                req.Content = new StringContent(JsonConvert.SerializeObject(productToAdd), Encoding.UTF8, "application/json");
                if (token != null && token.Length != 0)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var res = await client.SendAsync(req);
                if (res.IsSuccessStatusCode)
                {
                    //Success
                }
                else
                {
                    //Fail
                }
            }
            //Fail
        }

        public async Task<Order> CheckoutShoppingCartAsync(string user, string token)
        {
            if (user != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, ApiPath.OrdersPlace.GetEndpoint());
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

        public async Task<UserShoppingCartModel> GetUserShoppingCartAsync(string user, string token)
        {
            var client = _httpClient.CreateClient();
            var req = new HttpRequestMessage(HttpMethod.Post, ApiPath.ShoppingCarts.GetEndpoint());
            req.Content = new StringContent(JsonConvert.SerializeObject(new ShoppingCartRequestModel() { Identity = user }), Encoding.UTF8, "application/json");
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var res = await client.SendAsync(req);
            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserShoppingCartModel>(jsonString);
            }
            else
            {
                return null;
            }
        }
    }
}
