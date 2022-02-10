using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetColorsAsync(string token);
        Task<Color> GetColorAsync(int id, string token);
        Task CreateColorAsync(Color color, string token);
        Task UpdateColorAsync(int id, Color color, string token);
        Task DeleteColorAsync(int id, string token);
    }
}
