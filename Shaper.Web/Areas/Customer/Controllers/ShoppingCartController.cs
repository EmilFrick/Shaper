using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Models.ProductModels;
using Shaper.Web.Areas.Customer.Services.IServices;

namespace Shaper.Web.Areas.Customer.Controllers
{
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

    }
}
