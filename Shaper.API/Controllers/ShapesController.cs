using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ColorVM;
using Shaper.Models.ViewModels.ShapeVM;
using System.Data;
using System.Drawing;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ShapesController : ControllerBase
    {

        private readonly IUnitOfWork _db;

        public ShapesController(IUnitOfWork db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetShapes()
        {
            var result = await _db.Shapes.GetAllAsync(includeProperties: "Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetShape(int id)
        {
            var result = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShape(ShapeCreateVM shape)
        {
            if (ModelState.IsValid)
            {
                Shape conflict = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Name == shape.Name && x.HasFrame == shape.HasFrame);
                if (conflict is not null)
                {
                    var feedback = new ShapeUpdateVM(conflict);
                    return Conflict(feedback);
                }
                Shape s = shape.GetShapeFromCreateVM();
                _db.Shapes.Update(s);
                await _db.SaveAsync();

                var productsAssociated = await _db.Products.GetProductsAssociatedWith(s);
                if (productsAssociated.Count > 0)
                {
                    _db.Products.EvaluateProductPrices(productsAssociated);
                    _db.Products.UpdateProductPrices(productsAssociated);
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateShape(int id, ShapeUpdateVM shape)
        {
            if (shape.Id != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Shape conflict = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id != shape.Id && x.Name == shape.Name && x.HasFrame == shape.HasFrame);
                if (conflict is not null)
                {
                    var feedback = new ShapeUpdateVM(conflict);
                    return Conflict(feedback);
                }
                Shape s = shape.GetShapeFromUpdateVM();
                _db.Shapes.Update(s);
                await _db.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShape(int id)
        {
            var shape = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");

            if (shape is not null || shape.Name == "Default")

            {
                if (shape.Products?.Count > 0)
                {
                    await _db.Shapes.CheckDefaultShapeAsync();
                    await _db.Products.RebuildingProductsAsync(shape);
                }
                _db.Shapes.Remove(shape);
                await _db.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
