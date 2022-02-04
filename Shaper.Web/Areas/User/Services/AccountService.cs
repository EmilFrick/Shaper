using Microsoft.AspNetCore.Identity;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.UserVM;
using Shaper.Utility;

namespace Shaper.Web.Areas.User.Services
{
    public class AccountService : IAccountService
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;


        public AccountService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signinManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task ConfirmingRoles()
        {
            if (!await _roleManager.RoleExistsAsync(SD.UserType.Customer.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.UserType.Customer.ToString()));
            }
            if (!await _roleManager.RoleExistsAsync(SD.UserType.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.UserType.Admin.ToString()));
            }
            if (!await _roleManager.RoleExistsAsync(SD.UserType.Artist.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.UserType.Artist.ToString()));
            }
        }

        public async Task<IdentityResult> UserRegistration(UserRegisterVM registerVM)
        {
            ApplicationUser user = registerVM.VMtoUser();
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                await FinalizeRegistration(user);
            }
            return result;
        }

        private async Task FinalizeRegistration(ApplicationUser user)
        {
            await _signinManager.SignInAsync(user, isPersistent: false);
            await _userManager.AddToRoleAsync(user, user.SelectedRole);
        }
    }
}

