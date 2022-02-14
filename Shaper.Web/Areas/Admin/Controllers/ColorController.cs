using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.Models.ColorModels;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetColorsAsync(HttpContext.Session.GetString("JwToken"));
            var colorVMs = ColorDisplayVM.ColorDisplayVMs(colors);
            return View(colorVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var color = await _colorService.GetColorAsync(id, HttpContext.Session.GetString("JwToken"));
            return View(color);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ColorUpsertModel colorVM = new();
            if (id == 0 || id == null)
            {
                return View(colorVM);
            }
            else
            {
                Color color = await _colorService.GetColorAsync(id.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
                colorVM = new ColorUpsertModel(color);
                return View(colorVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ColorUpsertModel colorVM)
        {
            if (ModelState.IsValid)
            {
                var color = colorVM.GetColorFromUpdateVM();
                if (colorVM.Id == 0)
                {
                    await _colorService.CreateColorAsync(color, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _colorService.UpdateColorAsync(color.Id, color, HttpContext.Session.GetString("JwToken"));
                }
            }
            else
            {
                return View(colorVM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteColorAsync(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}