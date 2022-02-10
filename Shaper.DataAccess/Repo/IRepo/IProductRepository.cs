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
        Task RebuildingProductsAsync(Color color);
        Task RebuildingProductsAsync(Shape shape);
        Task RebuildingProductsAsync(Transparency transparency);


    }
}
