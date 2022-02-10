using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        Task<IEnumerable<Product>> UpdateCollectionWithDefaultShape(IEnumerable<Product> products);
        Task<IEnumerable<Product>> UpdateCollectionWithDefaultTransparency(IEnumerable<Product> products);
        Task RebuildingProducts(Color color);
        Task RebuildingProducts(Shape shape);
        Task RebuildingProducts(Transparency transparency);


    }
}
