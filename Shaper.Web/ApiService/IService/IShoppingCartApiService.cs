using Shaper.Models.Entities;
using Shaper.Models.Models.ShoppingCartModels;

namespace Shaper.Web.ApiService.IService
{
    public interface IShoppingCartApiService : IApiService<ShoppingCart>
    {
        Task<bool> AddingProductToUserShoppingCart(CartProductAddModel productToAdd, string url, string token = "");
    }
}