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
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<IdentityUser> signinManager, IAccountService accountService)
        {
            _signinManager = signinManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            UserRegisterVM registerUserVM = new();
            return View(registerUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> RedirectLogin(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM loginUser, string? returnurl = null)
        {
            var urlDirection = returnurl ?? Url.Content("~/User/Home");
            if (ModelState.IsValid)
            {
                var loginResult = await _accountService.ShaperLogin(loginUser);
                if (loginResult != null && loginResult.Token != null)
                {
                    HttpContext.Session.SetString("JwToken", loginResult.Token);
                    return LocalRedirect(urlDirection);
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
