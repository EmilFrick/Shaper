using Microsoft.AspNetCore.Mvc;

namespace Shaper.Web.Areas.Admin.Controllers
{
    public class TransparencyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }
    }
}
