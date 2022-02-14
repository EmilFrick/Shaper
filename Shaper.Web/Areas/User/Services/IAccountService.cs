using Microsoft.AspNetCore.Identity;
using Shaper.Models.Entities;
using Shaper.Models.Models.UserModels;

namespace Shaper.Web.Areas.User.Services
{
    public interface IAccountService
    {
        Task ConfirmingRolesAsync();
        Task<IdentityResult> UserRegistrationAsync(UserRegisterModel registerVM);
        Task<ApplicationUser> ShaperLoginAsync(UserLoginModel loginUser);
    }
}
