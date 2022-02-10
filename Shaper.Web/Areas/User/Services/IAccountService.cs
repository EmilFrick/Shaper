using Microsoft.AspNetCore.Identity;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.UserVM;

namespace Shaper.Web.Areas.User.Services
{
    public interface IAccountService
    {
        Task ConfirmingRolesAsync();
        Task<IdentityResult> UserRegistrationAsync(UserRegisterVM registerVM);
        Task<ApplicationUser> ShaperLoginAsync(UserLoginVM loginUser);
    }
}
