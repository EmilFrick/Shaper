using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.IdentityContext;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductComponentsVM;
using Shaper.Models.ViewModels.ProductVM;
using Shaper.Web.Areas.Artist.Services.IService;
using System.Security.Claims;

namespace Shaper.Web.Areas.Artist.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly IdentityAppDbContext _db;

        public ProductController(IProductService productService, IdentityAppDbContext db)
        {
            _productService = productService;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts(HttpContext.Session.GetString("JwToken"));
            var productVMs = ProductDisplayVM.GetProductDisplayVMs(products);
            return View(productVMs);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductUpsertVM productVM = new ProductUpsertVM();
            if (id == null)
            {
                productVM = await _productService.GetProductVMs(HttpContext.Session.GetString("JwToken"));
            }
            else
            {
                productVM = await _productService.GetProductVMs(HttpContext.Session.GetString("JwToken"), id);
            }
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductUpsertVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetProductWithComponents(productVM, HttpContext.Session.GetString("JwToken"));

                if (productVM.Id == 0)
                {
                    var currentArtistEmail = User.Identity.Name;
                    product.Artist = _db.ApplicationUsers.FirstOrDefault(x => x.Email == currentArtistEmail).FullName;
                    product.Created = DateTime.Now;
                    await _productService.CreateProduct(product, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _productService.UpdateProduct(product, HttpContext.Session.GetString("JwToken"));
                }
                return RedirectToAction("Index");
            }
            //not beautiful but will do for the time being.
            var refreshproduct = await _productService.GetProductVMs(HttpContext.Session.GetString("JwToken"));
            productVM.Colors = refreshproduct.Colors;
            productVM.Shapes = refreshproduct.Shapes;
            productVM.Transparencies = refreshproduct.Transparencies;

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}
