﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.TransparencyVM;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransparenciesController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public TransparenciesController(IUnitOfWork db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetTransparencies()
        {
            var result = await _db.Transparencies.GetAllAsync(includeProperties:"Products");
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTransparency(int id)
        {
            var result = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties:"Products");
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
                var result = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Name == transparency.Name || x.Value == transparency.Value);
                if (result is not null)
                {
                    return Conflict(result);
                }

                Transparency updateTransparency = transparency.GetTransparencyFromUpdateVM();
                _db.Transparencies.Update(updateTransparency);
                await _db.SaveAsync();
                
                return CreatedAtAction("GetTransparency", new { id = updateTransparency.Id }, updateTransparency);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTransparency(int id)
        {
            var result = await _db.Transparencies.GetFirstOrDefaultAsync(x => x.Id == id);
            if (result is not null)
            {
                _db.Transparencies.Remove(result);
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
