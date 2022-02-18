using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Models.OrderModels;
using Shaper.Models.Models.ProductModels;
using Shaper.Models.Models.ShoppingCartModels;
using Shaper.Web.Areas.Customer.Services.IServices;
using System.Data;

namespace Shaper.Web.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingService;

        public ShoppingCartController(IShoppingCartService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserShoppingCart()
        {
            var userShoppingCart = await _shoppingService.GetUserShoppingCartAsync(User.Identity.Name, HttpContext.Session.GetString("JwToken"));
            return View(userShoppingCart);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteProductFromShoppingCart(string itemname)
        {
            await _shoppingService.DeleteProductFromShoppingCart(itemname, User.Identity.Name, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("UserShoppingCart");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddToCart(ProductDisplayVM addingProduct)
        {
            await _shoppingService.AddProductToUsersCartAsync(addingProduct.Id, addingProduct.Quantity, User.Identity.Name, HttpContext.Session.GetString("JwToken"));
            return RedirectToRoute(new
            {
                controller = "CustomerProducts",
                action = "Details",
                id = addingProduct.Id
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var order = await _shoppingService.CheckoutShoppingCartAsync(User.Identity.Name, HttpContext.Session.GetString("JwToken"));
            var orderVM = new OrderDisplayVM(order);
            return View(orderVM);
        }
    }
}
