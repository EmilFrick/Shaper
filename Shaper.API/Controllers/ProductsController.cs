using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.Models.ProductComponentsModels;
using Shaper.Models.Models.ProductModels;

namespace Shaper.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public ProductsController(IUnitOfWork db)
        {
            _db = db;
        }


        [HttpGet("UpsertVM")]
        public async Task<IActionResult> GetProductVMs()
        {
            ProductUpsertModel productVM = new(await _db.Colors.GetAllAsync(),
                                            await _db.Shapes.GetAllAsync(),
                                            await _db.Transparencies.GetAllAsync());


            if (productVM.Colors == null || productVM.Shapes == null || productVM.Transparencies == null)
            {
                return Conflict();
            }
            return Ok(productVM);
        }

        [HttpGet("UpsertVM/{id:int}")]
        public async Task<IActionResult> GetProductVMs(int id)
        {
            var product = await _db.Products.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Color,Shape,Transparency");
            if (product == null)
            {
                return NotFound();
            }
            var productVM = new ProductUpsertModel(product,
                                                await _db.Colors.GetAllAsync(),
                                                await _db.Shapes.GetAllAsync(),
                                                await _db.Transparencies.GetAllAsync());

            if (productVM.Colors == null || productVM.Shapes == null || productVM.Transparencies == null)
            {
                return Conflict();
            }
            return Ok(productVM);
        }

        [HttpGet("ProductComponents")]
        public async Task<IActionResult> GetProductComponents(ProductReqComponentsModel request)
        {
            ProductResComponentsModel result = new()
            {
                ColorComponent = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == request.ColorId),
                ShapeComponent = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == request.ShapeId),
                TransparencyComponent = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id == request.TransparencyId)
            };
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _db.Products.GetAllAsync(includeProperties: "Color,Shape,Transparency");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _db.Products.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Color,Shape,Transparency");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateModel product)
        {
            var colorExists = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == product.ColorId);
            var shapeExists = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.ShapeId);
            var transparencyExists = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id == product.TransparencyId);
            if (colorExists is null || shapeExists is null || transparencyExists is null)
            {
                return Conflict(product);
            }

            if (ModelState.IsValid)
            {
                var result = await _db.Products.GetFirstOrDefaultAsync(x => x.Name == product.Name ||
                                                                            x.ColorId == product.ColorId && x.ShapeId == product.ShapeId && x.TransparencyId == product.TransparencyId);
                if (result is not null)
                {
                    return Conflict(result);
                }


                Product addProduct = product.GetProductFromCreateVM();
                await _db.Products.AddAsync(addProduct);
                await _db.SaveAsync();

                return Ok(addProduct);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateModel product)
        {
            if (product.Id != id)
            {
                return BadRequest();
            }

            var colorExists = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == product.ColorId);
            var shapeExists = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.ShapeId);
            var transparencyExists = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.TransparencyId);

            if (colorExists is null || shapeExists is null || transparencyExists is null)
            {
                return Conflict(product);
            }

            if (ModelState.IsValid)
            {
                var result = await _db.Products.GetFirstOrDefaultAsync(x => x.Id != product.Id & x.Name == product.Name ||
                                                                            x.Id != product.Id && x.ColorId == product.ColorId && x.ShapeId == product.ShapeId && x.TransparencyId == product.TransparencyId);

                if (result is not null)
                {
                    return Conflict(result);
                }

                var updateProduct = product.GetProductFromUpdateVM(product);
                _db.Products.Update(updateProduct);
                await _db.SaveAsync();

                return Ok(updateProduct);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _db.Products.GetFirstOrDefaultAsync(x => x.Id == id);
            if (result is not null)
            {
                _db.Products.Remove(result);
                await _db.SaveAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
