using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.TransparencyVM;
using Shaper.Web.Areas.Admin.Services.IService;

namespace Shaper.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TransparencyController : Controller
    {
        private readonly ITransparencyService _transparencyService;

        public TransparencyController(ITransparencyService transparencyService)
        {
            _transparencyService = transparencyService;
        }

        public async Task<IActionResult> Index()
        {
            var transparencys = await _transparencyService.GetTransparencys(HttpContext.Session.GetString("JwToken"));
            return View(transparencys);
        }

        public async Task<IActionResult> Details(int id)
        {
            var transparency = await _transparencyService.GetTransparency(id, HttpContext.Session.GetString("JwToken"));
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            TransparencyUpsertVM transparencyVM = new();
            if (id == 0 || id == null)
            {
                return View(transparencyVM);
            }
            else
            {
                Transparency transparency = await _transparencyService.GetTransparency(id.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
                transparencyVM = new TransparencyUpsertVM(transparency);
                return View(transparencyVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TransparencyUpsertVM transparencyVM)
        {
            if (ModelState.IsValid)
            {
                var transparency = transparencyVM.GetTransparencyFromUpdateVM();
                if (transparencyVM.Id == 0)
                {
                    await _transparencyService.CreateTransparency(transparency, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _transparencyService.UpdateTransparency(transparency.Id, transparency, HttpContext.Session.GetString("JwToken"));
                }
            }
            else
            {
                return View(transparencyVM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _transparencyService.DeleteTransparency(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}