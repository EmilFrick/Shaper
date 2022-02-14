using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.Models.ProductModels;
using Shaper.Web.Areas.Admin.Services.IService;
using Shaper.Web.Areas.Artist.Services.IService;

namespace Shaper.Web.Areas.Customer.Controllers
{
    public class CustomerProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IColorService _colorService;

        public CustomerProductsController(IProductService productService, IColorService colorService)
        {
            _productService = productService;
            _colorService = colorService;
        }

        public async Task<IActionResult> Index()
        {
            ProductCustomerIndexVM productsVM = new();
            productsVM.Products = await _productService.GetProductsAsync(HttpContext.Session.GetString("JwToken"));
            productsVM.SetListItems(await _colorService.GetColorsAsync(HttpContext.Session.GetString("JwToken")));
            return View(productsVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductCustomerIndexVM productsVM)
        {
            var colorExists = await _colorService.GetColorAsync(productsVM.ColorIdFilter.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
            if (colorExists is null)
                RedirectToAction("Index");
            
            productsVM.Products = await _productService.GetProductsByColorAsync(productsVM.ColorIdFilter.GetValueOrDefault(), HttpContext.Session.GetString("JwToken"));
            productsVM.SetListItems(await _colorService.GetColorsAsync(HttpContext.Session.GetString("JwToken")));
            return View(productsVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductAsync(id, HttpContext.Session.GetString("JwToken"));
            if(product is null)
                return RedirectToAction("Index");

            ProductDisplayVM productVM = new(product);
            return View(productVM);
        }
    }
}
