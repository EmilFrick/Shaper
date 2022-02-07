using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface IShapeService
    {
        Task<IEnumerable<Shape>> GetShapes(string token);
        Task<Shape> GetShape(int id, string token);
        Task CreateShape(Shape shape, string token);
        Task UpdateShape(int id, Shape shape, string token);
        Task DeleteShape(int id, string token);
    }
}
