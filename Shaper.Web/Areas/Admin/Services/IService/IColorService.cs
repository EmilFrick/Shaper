using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetColors(string token);
        Task<Color> GetColor(int id, string token);
        Task CreateColor(Color color, string token);
        Task UpdateColor(int id, Color color, string token);
        Task DeleteColor(int id, string token);
    }
}
