using Microsoft.AspNetCore.Identity;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.UserVM;

namespace Shaper.Web.Areas.User.Services
{
    public interface IAccountService
    {
        Task ConfirmingRoles();
        Task<IdentityResult> UserRegistration(UserRegisterVM registerVM);
        Task<bool> LoginShaperUser(UserLoginVM loginVM);
    }
}
