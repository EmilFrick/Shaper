using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ColorVM;

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
        public async Task<IActionResult> GetColors()
        {
            var result = await _db.Colors.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetColor(int id)
        {
            var result = await _db.Colors.GetFirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(ColorCreateVM color)
        {
            if (ModelState.IsValid)
            {
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
    }
}
