using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductComponentsVM;
using Shaper.Models.ViewModels.ProductVM;

namespace Shaper.Web.Areas.Artist.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(string token);
        Task<ProductUpsertVM> GetProductVMsAsync(string token, int? id = null);
        Task<Product> GetProductWithComponentsAsync(ProductUpsertVM productVM, string token);

        Task<Product> GetProductAsync(int id, string token);
        Task CreateProductAsync(Product product, string token);
        Task UpdateProductAsync(Product product, string token);
        Task DeleteProductAsync(int id, string token);
    }
}
