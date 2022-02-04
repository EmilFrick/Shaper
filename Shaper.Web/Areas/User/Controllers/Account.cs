using Microsoft.AspNetCore.Mvc;

namespace Shaper.Web.Areas.User.Controllers
{
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
