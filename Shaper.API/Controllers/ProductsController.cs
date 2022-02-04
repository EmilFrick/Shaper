﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductVM;

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
        public async Task<IActionResult> CreateProduct(ProductCreateVM product)
        {
            var colorExists = _db.Colors.GetFirstOrDefaultAsync(x => x.Id == product.ColorId);
            var shapeExists = _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.ShapeId);
            var transparencyExists = _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.TransparencyId);
            if (colorExists is null || shapeExists is null || transparencyExists is null)
            {
                return Conflict(product);
            }
            
            if (ModelState.IsValid)
            {
                var result = await _db.Products.GetFirstOrDefaultAsync(x => x.Name == product.Name ||
                                                                           (x.ColorId == product.ColorId &&
                                                                            x.ShapeId == product.ShapeId &&
                                                                            x.TransparencyId == product.TransparencyId));
                if (result is not null)
                {
                    return Conflict(result);
                }


                Product addProduct = product.GetProductFromCreateVM();
                await _db.Products.AddAsync(addProduct);
                await _db.SaveAsync();

                return CreatedAtAction("GetProduct", new { id = addProduct.Id }, addProduct);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateVM product)
        {
            if (product.Id != id)
            {
                return BadRequest();
            }

            var colorExists = _db.Colors.GetFirstOrDefaultAsync(x => x.Id == product.ColorId);
            var shapeExists = _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.ShapeId);
            var transparencyExists = _db.Shapes.GetFirstOrDefaultAsync(x => x.Id == product.TransparencyId);
            if (colorExists is null || shapeExists is null || transparencyExists is null)
            {
                return Conflict(product);
            }

            if (ModelState.IsValid)
            {
                var result = await _db.Products.GetFirstOrDefaultAsync(x => x.Name == product.Name ||
                                                                           (x.ColorId == product.ColorId &&
                                                                            x.ShapeId == product.ShapeId &&
                                                                            x.TransparencyId == product.TransparencyId));
                if (result is not null)
                {
                    return Conflict(result);
                }



                Product updateProduct = product.GetProductFromUpdateVM(result);
                _db.Products.Update(updateProduct);
                await _db.SaveAsync();

                return CreatedAtAction("GetProduct", new { id = updateProduct.Id }, updateProduct);
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
