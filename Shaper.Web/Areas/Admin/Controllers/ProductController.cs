using Microsoft.AspNetCore.Mvc;

namespace Shaper.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Index()
        //{
            
        //    return View();
        //}
    }
}
