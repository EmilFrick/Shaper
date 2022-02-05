using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetColors();

    }
}
