using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface IShapeService
    {
        Task<IEnumerable<Shape>> GetShapesAsync(string token);
        Task<Shape> GetShapeAsync(int id, string token);
        Task CreateShapeAsync(Shape shape, string token);
        Task UpdateShapeAsync(int id, Shape shape, string token);
        Task DeleteShapeAsync(int id, string token);
    }
}
