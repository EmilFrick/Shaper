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

        public async Task<Transparency> GetTransparency(int id, string token)
        {
            return await _apiService.TransparencyApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Transparencies.GetEndpoint(id), token);
        }

        public async Task<IEnumerable<Transparency>> GetTransparencys(string token)
        {
            return await _apiService.TransparencyApi.GetAllAsync(ApiPaths.ApiPath.Transparencies.GetEndpoint(null), token);
        }

        public async Task CreateTransparency(Transparency transparency, string token)
        {
            await _apiService.TransparencyApi.AddAsync(transparency, ApiPaths.ApiPath.Transparencies.GetEndpoint(null), token);
        }

        public async Task UpdateTransparency(int id, Transparency transparency, string token)
        {
            await _apiService.TransparencyApi.UpdateAsync(transparency, ApiPaths.ApiPath.Transparencies.GetEndpoint(id), token);
        }

        public async Task DeleteTransparency(int id, string token)
        {
            await _apiService.TransparencyApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Transparencies.GetEndpoint(id), token);
        }
    }
}
