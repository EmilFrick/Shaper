using Shaper.Models.Entities;
using Shaper.Utility;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Services
{
    public class ColorService : IColorService
    {
        private readonly IShaperApiService _apiService;

        public ColorService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<Color> GetColor(int id, string token)
        {
            return await _apiService.ColorApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Colors.GetEndpoint(id), token);
        }

        public async Task<IEnumerable<Color>> GetColors(string token)
        {
            return await _apiService.ColorApi.GetAllAsync(ApiPaths.ApiPath.Colors.GetEndpoint(), token);
        }

        public async Task CreateColor(Color color, string token)
        {
            await _apiService.ColorApi.AddAsync(color, ApiPaths.ApiPath.Colors.GetEndpoint(), token);
        }

        public async Task UpdateColor(int id, Color color, string token)
        {
            await _apiService.ColorApi.UpdateAsync(color, ApiPaths.ApiPath.Colors.GetEndpoint(id), token);
        }

        public async Task DeleteColor(int id, string token)
        {
            await _apiService.ColorApi.RemoveAsync(ApiPaths.ApiPath.Colors.GetEndpoint(id), token);
        }
    }
}
