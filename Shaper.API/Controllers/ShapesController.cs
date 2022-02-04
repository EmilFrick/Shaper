using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShapeVM;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapesController : ControllerBase
    {

        private readonly IUnitOfWork _db;

        public ShapesController(IUnitOfWork db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetShapes()
        {
            var result = await _db.Shapes.GetAllAsync(includeProperties:"Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetShape(int id)
        {
            var result = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties:"Products");
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
                var result = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Name == shape.Name);
                if (result is not null)
                {
                    return Conflict(result);
                }

                Shape addShape = shape.GetShapeFromCreateVM();
                await _db.Shapes.AddAsync(addShape);
                await _db.SaveAsync();

                return CreatedAtAction("GetShape", new { id = addShape.Id }, addShape);
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
                var result = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Name == shape.Name);
                if (result is not null)
                {
                    return Conflict(result);
                }

                Shape updateShape = shape.GetShapeFromUpdateVM();
                _db.Shapes.Update(updateShape);
                await _db.SaveAsync();
                
                return CreatedAtAction("GetShape", new { id = updateShape.Id }, updateShape);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShape(int id)
        {
            var result = await _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == id);
            if (result is not null)
            {
                _db.Shapes.Remove(result);
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
