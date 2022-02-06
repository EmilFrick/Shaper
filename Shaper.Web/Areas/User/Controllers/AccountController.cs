using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.UserVM;
using Shaper.Utility;
using Shaper.Web.Areas.User.Services;
using Shaper.Web.Controllers;

namespace Shaper.Web.Areas.User.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IAccountService _accountService;



        public AccountController(SignInManager<IdentityUser> signinManager, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IAccountService accountService)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            UserRegisterVM registerUserVM = new();

            return View(registerUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                await _accountService.ConfirmingRoles();
                var registration = await _accountService.UserRegistration(userVM);
                if (registration.Succeeded)
                {
                    return LocalRedirect(Url.Content("~/User/Home"));
                }
            }
            return View(userVM);
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM loginUser, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/User/Home");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var loginResult = await _accountService.ShaperLogin(loginUser);
                if (loginResult != null && loginResult.Token != null)
                {
                    HttpContext.Session.SetString("JwToken", loginResult.Token);
                    return LocalRedirect(returnUrl);
                }
                return View(loginUser);
            }

            ModelState.AddModelError(string.Empty, "Unable to login.");
            return View(loginUser);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            HttpContext.Session.SetString("JwToken", "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
