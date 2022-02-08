using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductVM;

namespace Shaper.Web.Areas.Artist.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(string token);
        Task<ProductUpsertVM> GetProductVMs(string token, int? id = null);

        Task<Product> GetProduct(int id, string token);
        Task CreateProduct(Product product, string token);
        Task UpdateProduct(int id, Product product, string token);
        Task DeleteProduct(int id, string token);
    }
}
