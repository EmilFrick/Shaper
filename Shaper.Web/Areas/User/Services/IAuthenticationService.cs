using Shaper.Models.Entities;

namespace Shaper.Web.Areas.User.Services
{
    public interface IAuthenticationService
    {
        void Authentication(ApplicationUser user);
    }
}
