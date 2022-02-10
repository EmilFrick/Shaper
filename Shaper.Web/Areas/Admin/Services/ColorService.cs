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

        public async Task<Color> GetColorAsync(int id, string token)
        {
            return await _apiService.ColorApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Colors.GetEndpoint(id), token);
        }

        public async Task<IEnumerable<Color>> GetColorsAsync(string token)
        {
            return await _apiService.ColorApi.GetAllAsync(ApiPaths.ApiPath.Colors.GetEndpoint(), token);
        }

        public async Task CreateColorAsync(Color color, string token)
        {
            await _apiService.ColorApi.AddAsync(color, ApiPaths.ApiPath.Colors.GetEndpoint(), token);
        }

        public async Task UpdateColorAsync(int id, Color color, string token)
        {
            await _apiService.ColorApi.UpdateAsync(color, ApiPaths.ApiPath.Colors.GetEndpoint(id), token);
        }

        public async Task DeleteColorAsync(int id, string token)
        {
            await _apiService.ColorApi.RemoveAsync(ApiPaths.ApiPath.Colors.GetEndpoint(id), token);
        }
    }
}
