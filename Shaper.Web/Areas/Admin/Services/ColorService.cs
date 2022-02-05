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

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _apiService.ColorApi.GetAllAsync(ApiPaths.ApiPath.Colors.GetEndpoint(null));

        }
    }
}
