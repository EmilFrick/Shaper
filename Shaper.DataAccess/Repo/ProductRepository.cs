using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using SnutteBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        private void EvaluateProductPrices(List<Product> products)
        {
            products.ForEach(fix => fix.Price = fix.Color.AddedValue + fix.Shape.AddedValue + fix.Transparency.AddedValue);
        }

        public async Task RebuildingProducts(Color color)
        {
            var products = await _db.Products.Include(c => c.Color).Include(s => s.Shape).Include(t => t.Transparency).Where(x => x.ColorId == color.Id).ToListAsync();
            await UpdateCollectionWithDefaultColor(products);
            EvaluateProductPrices(products);
            _db.UpdateRange(products);
            await _db.SaveChangesAsync();
        }

        public async Task RebuildingProducts(Shape shape)
        {
            var products = await _db.Products.Include(c => c.Color).Include(s => s.Shape).Include(t => t.Transparency).Where(x => x.ShapeId == shape.Id).ToListAsync();
            await UpdateCollectionWithDefaultShape(products);
            EvaluateProductPrices(products);
            _db.UpdateRange(products);
            await _db.SaveChangesAsync();
        }

        public async Task RebuildingProducts(Transparency transparency)
        {
            var products = await _db.Products.Include(c => c.Color).Include(s => s.Shape).Include(t => t.Transparency).Where(x => x.TransparencyId == transparency.Id).ToListAsync();
            await UpdateCollectionWithDefaultTransparency(products);
            EvaluateProductPrices(products);
            _db.UpdateRange(products);
            await _db.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }

        private async Task UpdateCollectionWithDefaultColor(List<Product> products)
        {
            var defaultColor = await _db.Colors.FirstOrDefaultAsync(x => x.Name == "Default");
            foreach (var product in products)
            {
                product.Color = defaultColor;
                product.ColorId = defaultColor.Id;
            }

            //_db.UpdateRange(products);
            //_db.SaveChangesAsync();
        }


        public async Task<IEnumerable<Product>> UpdateCollectionWithDefaultShape(IEnumerable<Product> products)
        {
            var defaultShape = await _db.Shapes.FirstOrDefaultAsync(x => x.Name == "Default");
            foreach (var product in products)
            {
                if (product.ShapeId == 0)
                {
                    product.Shape = defaultShape;
                    product.ShapeId = defaultShape.Id;
                }
            }
            //_db.UpdateRange(products);
            //_db.SaveChangesAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> UpdateCollectionWithDefaultTransparency(IEnumerable<Product> products)
        {
            var defaultTransparency = await _db.Transparencies.FirstOrDefaultAsync(x => x.Name == "Default");

            foreach (var product in products)
            {
                if (product.TransparencyId == 0)
                {
                    product.Transparency = defaultTransparency;
                    product.TransparencyId = defaultTransparency.Id;
                }
            }

            //_db.UpdateRange(products);
            //_db.SaveChangesAsync();
            return products;
        }


    }
}
