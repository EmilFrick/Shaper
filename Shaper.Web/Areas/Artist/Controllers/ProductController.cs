using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.IdentityContext;
using Shaper.Models.Entities;
using Shaper.Models.Models.ProductModels;
using Shaper.Web.Areas.Artist.Services.IService;
using System.Data;
using System.Security.Claims;

namespace Shaper.Web.Areas.Artist.Controllers
{
    [Authorize(Roles = "Artist")]
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
            var products = await _productService.GetProductsAsync(HttpContext.Session.GetString("JwToken"));
            var productVMs = ProductDisplayVM.GetProductDisplayVMs(products);
            return View(productVMs);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductUpsertModel productVM = new ProductUpsertModel();
            if (id == null)
            {
                productVM = await _productService.GetProductVMsAsync(HttpContext.Session.GetString("JwToken"));
            }
            else
            {
                productVM = await _productService.GetProductVMsAsync(HttpContext.Session.GetString("JwToken"), id);
            }
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductUpsertModel productVM)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.GetProductWithComponentsAsync(productVM, HttpContext.Session.GetString("JwToken"));

                if (productVM.Id == 0)
                {
                    var currentArtistEmail = User.Identity.Name;
                    product.Artist = _db.ApplicationUsers.FirstOrDefault(x => x.Email == currentArtistEmail).FullName;
                    product.Created = DateTime.Now;
                    await _productService.CreateProductAsync(product, HttpContext.Session.GetString("JwToken"));
                }
                else
                {
                    await _productService.UpdateProductAsync(product, HttpContext.Session.GetString("JwToken"));
                }
                return RedirectToAction("Index");
            }
            //not beautiful but will do for the time being.
            var refreshproduct = await _productService.GetProductVMsAsync(HttpContext.Session.GetString("JwToken"));
            productVM.Colors = refreshproduct.Colors;
            productVM.Shapes = refreshproduct.Shapes;
            productVM.Transparencies = refreshproduct.Transparencies;

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id, HttpContext.Session.GetString("JwToken"));
            return RedirectToAction("Index");
        }
    }
}
