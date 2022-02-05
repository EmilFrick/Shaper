using Shaper.Models.Entities;

namespace Shaper.Web.Areas.User.Services
{
    public interface IAuthenticationService
    {
        Task HandingOverToken(ApplicationUser user);
    }
}
