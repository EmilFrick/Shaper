using Microsoft.AspNetCore.Mvc;

namespace Shaper.Web.Areas.Customer.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
