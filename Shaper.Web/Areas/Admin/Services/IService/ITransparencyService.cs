using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Admin.Services.IService
{
    public interface ITransparencyService
    {
        Task<IEnumerable<Transparency>> GetTransparencysAsync(string token);
        Task<Transparency> GetTransparencyAsync(int id, string token);
        Task CreateTransparencyAsync(Transparency transparency, string token);
        Task UpdateTransparencyAsync(int id, Transparency transparency, string token);
        Task DeleteTransparencyAsync(int id, string token);
    }
}
