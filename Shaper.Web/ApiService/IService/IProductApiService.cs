using Shaper.Models.Entities;
using Shaper.Models.Models.ProductComponentsModels;
using Shaper.Models.Models.ProductModels;

namespace Shaper.Web.ApiService.IService
{
    public interface IProductApiService : IApiService<Product>
    {
        Task<ProductUpsertModel> FetchVMAsync(string url, string token = "");
        Task<ProductResComponentsModel> FetchProductComponentsAsync(ProductReqComponentsModel reqModel, string url, string token = "");
    }
}
