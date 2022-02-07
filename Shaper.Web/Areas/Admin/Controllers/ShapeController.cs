using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShapeVM;
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
            var shapes = await _shapeService.GetShapes(HttpContext.Session.GetString("JwToken"));
            return View(shapes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var shape = await _shapeService.GetShape(id, HttpContext.Session.GetString("JwToken"));
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ShapeUpsertVM shapeVM = new();
            if (id == 0 || id == null)
            {
                return View(shapeVM);
            }
            else
            {
                Shape shape = await _shapeService.GetShape(id.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
                shapeVM = new ShapeUpsertVM(shape);
                return View(shapeVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ShapeUpsertVM shapeVM)
        {
            if (ModelState.IsValid)
            {
                var shape = shapeVM.GetShapeFromUpdateVM();
                if (shapeVM.Id == 0)
                {
                    await _shapeService.CreateShape(shape, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _shapeService.UpdateShape(shape.Id, shape, HttpContext.Session.GetString("JwToken"));
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
            await _shapeService.DeleteShape(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}