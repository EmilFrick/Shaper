using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.Models.TransparencyModels;
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
            var transparencies = await _transparencyService.GetTransparencysAsync(HttpContext.Session.GetString("JwToken"));
            var transparenciesVM = TransparencyDisplayVM.TransparencyDisplayVMs(transparencies);
            return View(transparenciesVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var transparency = await _transparencyService.GetTransparencyAsync(id, HttpContext.Session.GetString("JwToken"));
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            TransparencyUpsertModel transparencyVM = new();
            if (id == 0 || id == null)
            {
                return View(transparencyVM);
            }
            else
            {
                Transparency transparency = await _transparencyService.GetTransparencyAsync(id.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
                transparencyVM = new TransparencyUpsertModel(transparency);
                return View(transparencyVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TransparencyUpsertModel transparencyVM)
        {
            if (ModelState.IsValid)
            {
                var transparency = transparencyVM.GetTransparencyFromUpdateVM();
                if (transparencyVM.Id == 0)
                {
                    await _transparencyService.CreateTransparencyAsync(transparency, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _transparencyService.UpdateTransparencyAsync(transparency.Id, transparency, HttpContext.Session.GetString("JwToken"));
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
            await _transparencyService.DeleteTransparencyAsync(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}