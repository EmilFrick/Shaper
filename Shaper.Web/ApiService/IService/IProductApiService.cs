using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductVM;

namespace Shaper.Web.ApiService.IService
{
    public interface IProductApiService : IApiService<Product>
    {
        Task<ProductUpsertVM> FetchVM(string url, string token = "");
    }
}
