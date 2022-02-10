﻿using Newtonsoft.Json;
using NuGet.Common;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductComponentsVM;
using Shaper.Models.ViewModels.ProductVM;
using Shaper.Web.ApiService.IService;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace Shaper.Web.ApiService
{
    public class ProductApiService : ApiService<Product>, IProductApiService
    {

        private readonly IHttpClientFactory _httpClient;

        public ProductApiService(IHttpClientFactory httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProductUpsertVM> FetchVMAsync(string url, string token = "")
        {
            var client = _httpClient.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var res = await client.SendAsync(req);

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductUpsertVM>(jsonString);
            }
            return null;
        }

        public async Task<ProductResComponentsVM> FetchProductComponentsAsync(ProductReqComponentsVM reqModel, string url, string token = "")
        {
            var client = _httpClient.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Content = new StringContent(JsonConvert.SerializeObject(reqModel), Encoding.UTF8, "application/json");
            var res = await client.SendAsync(req);

            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductResComponentsVM>(jsonString);
            }
            return null;
        }
    }
}
