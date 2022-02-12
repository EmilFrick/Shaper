using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers.IRequestHandlers
{
    public interface IShaperUserHandler
    {
        Task<ShaperUser> FindShaperUserByIdentityAsync(string identity);
    }
}