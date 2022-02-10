using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.TransparencyVM;
using System.Data;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class TransparenciesController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public TransparenciesController(IUnitOfWork db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTransparencies()
        {
            var result = await _db.Transparencies.GetAllAsync(includeProperties: "Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTransparency(int id)
        {
            var result = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransparency(TransparencyCreateVM transparency)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Name == transparency.Name || x.Value == transparency.Value);
                if (result is not null)
                {
                    return Conflict(result);
                }

                Transparency addTransparency = transparency.GetTransparencyFromCreateVM();
                await _db.Transparencies.AddAsync(addTransparency);
                await _db.SaveAsync();

                return CreatedAtAction("GetTransparency", new { id = addTransparency.Id }, addTransparency);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTransparency(int id, TransparencyUpdateVM transparency)
        {
            if (transparency.Id != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Transparency conflict = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id != transparency.Id && x.Name == transparency.Name ||
                                                                                        x.Id != transparency.Id && x.Value == transparency.Value);
                if (conflict is not null)
                {
                    var feedback = new TransparencyUpdateVM(conflict);
                    return Conflict(feedback);
                }
                Transparency c = transparency.GetTransparencyFromUpdateVM();
                _db.Transparencies.Update(c);
                await _db.SaveAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTransparency(int id)
        {
            var transparency = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Products");

            if (transparency is not null || transparency.Name == "Default")

            {
                if (transparency.Products?.Count > 0)
                {
                    await _db.Transparencies.CheckDefaultTransparency();
                    await _db.Products.RebuildingProducts(transparency);
                }
                _db.Transparencies.Remove(transparency);
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
