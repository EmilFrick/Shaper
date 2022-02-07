using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ColorVM;
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
            var colors = await _colorService.GetColors(HttpContext.Session.GetString("JwToken"));
            return View(colors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var color = await _colorService.GetColor(id, HttpContext.Session.GetString("JwToken"));
            return View(color);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ColorUpsertVM colorVM = new();
            if (id == 0)
            {
                return View(colorVM);
            }
            else
            {
                Color color = await _colorService.GetColor(id.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
                colorVM = new ColorUpsertVM(color);
                return View(colorVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ColorUpsertVM colorVM)
        {
            if (ModelState.IsValid)
            {
                var color = colorVM.GetColorFromUpdateVM();
                if (colorVM.Id == 0)
                {
                    await _colorService.CreateColor(color, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _colorService.UpdateColor(color.Id, color, HttpContext.Session.GetString("JwToken"));
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
            await _colorService.DeleteColor(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}