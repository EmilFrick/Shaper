using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.Models.ShapeModels;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShapeController : Controller
    {
        private readonly IShapeService _shapeService;

        public ShapeController(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        public async Task<IActionResult> Index()
        {
            var shapes = await _shapeService.GetShapesAsync(HttpContext.Session.GetString("JwToken"));
            var shapeVMs = ShapeDisplayVM.ShapeDisplayVMs(shapes);
            return View(shapeVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var shape = await _shapeService.GetShapeAsync(id, HttpContext.Session.GetString("JwToken"));
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ShapeUpsertModel shapeVM = new();
            if (id == 0 || id == null)
            {
                return View(shapeVM);
            }
            else
            {
                Shape shape = await _shapeService.GetShapeAsync(id.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
                shapeVM = new ShapeUpsertModel(shape);
                return View(shapeVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ShapeUpsertModel shapeVM)
        {
            if (ModelState.IsValid)
            {
                var shape = shapeVM.GetShapeFromUpdateVM();
                if (shapeVM.Id == 0)
                {
                    await _shapeService.CreateShapeAsync(shape, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _shapeService.UpdateShapeAsync(shape.Id, shape, HttpContext.Session.GetString("JwToken"));
                }
            }
            else
            {
                return View(shapeVM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _shapeService.DeleteShapeAsync(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}