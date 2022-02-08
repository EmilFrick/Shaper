using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductVM;
using Shaper.Web.Areas.Artist.Services.IService;

namespace Shaper.Web.Areas.Artist.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
                if (productVM.Id == null || productVM.Id == 0)
                {
                    
                }
                else
                {

                }
            }
            //not beautiful but will do for the time being.
            var refreshproduct = await _productService.GetProductVMs(HttpContext.Session.GetString("JwToken"));
            productVM.Colors = refreshproduct.Colors;
            productVM.Shapes = refreshproduct.Shapes;
            productVM.Transparencies = refreshproduct.Transparencies;

            return View(productVM);
        }
    }
}
