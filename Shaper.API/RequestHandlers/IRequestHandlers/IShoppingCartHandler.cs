using Shaper.Models.Entities;
using Shaper.Models.Models.ShoppingCartModels;

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
        Task RemoveItemFromShoppingCartAsync(int cartId, int productId);
        Task CheckOutShoppingCartAsync(string user);
        Task<ShoppingCart> GetShoppingCartByIDAsync(int id);
        Task UpdateShoppingCartAsync(int id, ShoppingCartUpdateModel cart);
        Task<ShoppingCart> RemoveShoppingCart(int id);
    }
}