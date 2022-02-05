using Newtonsoft.Json;
using Shaper.Web.ApiService.IService;
using System.Net.Http.Headers;

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
            var res = client.SendAsync(req).Result;
            if (res.IsSuccessStatusCode)
            {
                var jsonString = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }

            return null;
        }
        public Task<T> GetFirstOrDefaultAsync(int id, string url, string token = "")
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity, string url, string token = "")
        {
            throw new NotImplementedException();
        }


        public Task Update(int id, T entity, string url, string token = "")
        {
            throw new NotImplementedException();
        }
        public void Remove(T entity, string url, string token = "")
        {
            throw new NotImplementedException();
        }
    }
}
