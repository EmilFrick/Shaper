using Shaper.Models.Entities;
using Shaper.Utility;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Services
{
    public class ShapeService : IShapeService
    {
        private readonly IShaperApiService _apiService;

        public ShapeService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<Shape> GetShape(int id, string token)
        {
            return await _apiService.ShapeApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Shapes.GetEndpoint(id), token);
        }

        public async Task<IEnumerable<Shape>> GetShapes(string token)
        {
            return await _apiService.ShapeApi.GetAllAsync(ApiPaths.ApiPath.Shapes.GetEndpoint(), token);
        }

        public async Task CreateShape(Shape shape, string token)
        {
            await _apiService.ShapeApi.AddAsync(shape, ApiPaths.ApiPath.Shapes.GetEndpoint(), token);
        }

        public async Task UpdateShape(int id, Shape shape, string token)
        {
            await _apiService.ShapeApi.UpdateAsync(shape, ApiPaths.ApiPath.Shapes.GetEndpoint(id), token);
        }

        public async Task DeleteShape(int id, string token)
        {
            await _apiService.ShapeApi.RemoveAsync(ApiPaths.ApiPath.Shapes.GetEndpoint(id), token);
        }
    }
}
