using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface ITransparencyService
    {
        Task<IEnumerable<Transparency>> GetTransparencys(string token);
        Task<Transparency> GetTransparency(int id, string token);
        Task CreateTransparency(Transparency transparency, string token);
        Task UpdateTransparency(int id, Transparency transparency, string token);
        Task DeleteTransparency(int id, string token);
    }
}
