using Newtonsoft.Json;
using Shaper.Web.ApiService.IService;
using System.Net.Http.Headers;
using System.Text;

namespace Shaper.Web.ApiService
{
    public class ApiService<T> : IApiService<T> where T : class
    {

        private readonly IHttpClientFactory _httpClient;

        public ApiService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url, string token = "")
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
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }

            return null;
        }
        public async Task<T> GetFirstOrDefaultAsync(string url, string token = "")
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
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<bool> AddAsync(T entity, string url, string token = "")
        {
            if (entity != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                req.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
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


        public async Task<bool> UpdateAsync(T entity, string url, string token = "")
        {
            if (entity != null)
            {
                var client = _httpClient.CreateClient();
                var req = new HttpRequestMessage(HttpMethod.Put, url);
                req.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
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
        public async Task<bool> RemoveAsync(string url, string token = "")
        {
            var client = _httpClient.CreateClient();
            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
            var res = await client.SendAsync(req);

            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
