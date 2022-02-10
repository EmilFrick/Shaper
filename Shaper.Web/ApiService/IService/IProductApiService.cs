using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductComponentsVM;
using Shaper.Models.ViewModels.ProductVM;

namespace Shaper.Web.ApiService.IService
{
    public interface IProductApiService : IApiService<Product>
    {
        Task<ProductUpsertVM> FetchVMAsync(string url, string token = "");
        Task<ProductResComponentsVM> FetchProductComponentsAsync(ProductReqComponentsVM reqModel, string url, string token = "");
    }
}
