using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShoppingCartVM;

namespace Shaper.API.RequestHandlers.IRequestHandlers
{
    public interface IShoppingCartHandler
    {
        Task<ShoppingCart> GetUserShoppingCartAsync(string user);
        Task AddNewCartProductAsync(CartProductAddModel productModel, double unitprice, int shoppingcartId);
        Task UpdateCartProductAsync(CartProduct product, int quantityUpdate);
        Task<ShoppingCart> ShoppingCartExistAsync(string user);
        Task CalulatingShoppingCartValue(ShoppingCart cartId);
        Task<ShoppingCart> GetFreshShoppingCartAsync(string user);
        Task RemoveItemFromShoppingCart(int cartId, int productId);
        Task CheckOutShoppingCart(string user);
    }
}