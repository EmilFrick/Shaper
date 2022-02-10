using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.IdentityContext;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.UserVM;
using Shaper.Utility;
using Microsoft.AspNetCore.Http;

namespace Shaper.Web.Areas.User.Services
{
    public class AccountService : IAccountService
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IdentityAppDbContext _db;

        public AccountService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signinManager, IAuthenticationService authenticationService, IdentityAppDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signinManager = signinManager;
            _authenticationService = authenticationService;
            _db = db;
        }

        public async Task ConfirmingRolesAsync()
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

        public async Task<IdentityResult> UserRegistrationAsync(UserRegisterVM registerVM)
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
            await _userManager.AddToRoleAsync(user, user.Role);
        }

        public async Task<ApplicationUser> ShaperLoginAsync(UserLoginVM loginUser)
        {
            var user = await GetApplicationUser(loginUser);
            if (user == null)
                return null;
            
            var userLogin = await _signinManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, lockoutOnFailure: false);
            if (!userLogin.Succeeded)
                return null;

            await _authenticationService.HandingOverTokenAsync(user);
            if (user.Token == null)
                return null;

            return user;
        }

        private async Task<ApplicationUser> GetApplicationUser(UserLoginVM loginUser)
        {
            return await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == loginUser.Email);
        }
    }
}

