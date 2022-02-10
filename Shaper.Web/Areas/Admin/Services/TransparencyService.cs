using Shaper.Models.Entities;
using Shaper.Utility;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Services
{
    public class TransparencyService : ITransparencyService
    {
        private readonly IShaperApiService _apiService;

        public TransparencyService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<Transparency> GetTransparencyAsync(int id, string token)
        {
            return await _apiService.TransparencyApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Transparencies.GetEndpoint(id), token);
        }

        public async Task<IEnumerable<Transparency>> GetTransparencysAsync(string token)
        {
            return await _apiService.TransparencyApi.GetAllAsync(ApiPaths.ApiPath.Transparencies.GetEndpoint(), token);
        }

        public async Task CreateTransparencyAsync(Transparency transparency, string token)
        {
            await _apiService.TransparencyApi.AddAsync(transparency, ApiPaths.ApiPath.Transparencies.GetEndpoint(), token);
        }

        public async Task UpdateTransparencyAsync(int id, Transparency transparency, string token)
        {
            await _apiService.TransparencyApi.UpdateAsync(transparency, ApiPaths.ApiPath.Transparencies.GetEndpoint(id), token);
        }

        public async Task DeleteTransparencyAsync(int id, string token)
        {
            await _apiService.TransparencyApi.RemoveAsync(ApiPaths.ApiPath.Transparencies.GetEndpoint(id), token);
        }
    }
}
