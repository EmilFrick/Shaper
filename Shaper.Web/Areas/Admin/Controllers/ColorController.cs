using Microsoft.AspNetCore.Mvc;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Controllers
{
    public class ColorController : Controller
    {

        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetColors();
            return View(colors);
        }

    }
}
