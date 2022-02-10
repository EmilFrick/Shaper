using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ColorVM;
using System.Data;
using System.Drawing;
using Color = Shaper.Models.Entities.Color;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    [ApiController]
    public class ColorsController : ControllerBase
    {

        private readonly IUnitOfWork _db;

        public ColorsController(IUnitOfWork db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var result = await _db.Colors.GetAllAsync(includeProperties: "Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetColor(int id)
        {
            var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");
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
                Color conflict = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id != color.Id && x.Hex == color.Hex ||
                                                                          x.Id != color.Id && x.Name == color.Name);
                if (conflict is not null)
                {
                    var feedback = new ColorUpdateVM(conflict);
                    return Conflict(feedback);
                }
                Color c = color.GetColorFromUpdateVM();
                _db.Colors.Update(c);
                await _db.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");

            if (color is not null || color.Name == "Default")

            {
                if (color.Products?.Count > 0)
                {
                    await _db.Colors.CheckDefaultColor();
                    await _db.Products.RebuildingProducts(color);
                }
                _db.Colors.Remove(color);
                await _db.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            //    var defaultColor = await _db.Colors.CheckDefaultColor();
            //    await _db.Products.UpdateCollectionWithDefaultColor(result.Products, defaultColor);


            //var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");
            //if (result is not null)
            //{
            //    if (result.Products.Count > 0)
            //    {
            //    }


            //_db.Colors.Remove(result);
            //await _db.SaveAsync();

        }
    }
}
