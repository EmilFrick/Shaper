using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ColorVM;
using System.Data;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {

        private readonly IUnitOfWork _db;

        public ColorsController(IUnitOfWork db)
        {
            _db = db;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetColors()
        {
            var result = await _db.Colors.GetAllAsync(includeProperties:"Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetColor(int id)
        {
            var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties:"Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(ColorCreateVM color)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Name == color.Name || x.Hex == color.Hex);
                if (result is not null)
                {
                    return Conflict(result);
                }

                Color addColor = color.GetColorFromCreateVM();
                await _db.Colors.AddAsync(addColor);
                await _db.SaveAsync();

                return CreatedAtAction("GetColor", new { id = addColor.Id }, addColor);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateColor(int id, ColorUpdateVM color)
        {
            if (color.Id != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Name == color.Name || x.Hex == color.Hex);
                if (result is not null)
                {
                    return Conflict(result);
                }

                Color updateColor = color.GetColorFromUpdateVM();
                _db.Colors.Update(updateColor);
                await _db.SaveAsync();
                
                return CreatedAtAction("GetColor", new { id = updateColor.Id }, updateColor);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == id);
            if (result is not null)
            {
                _db.Colors.Remove(result);
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
