using Microsoft.AspNetCore.Mvc;

namespace Shaper.Web.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
