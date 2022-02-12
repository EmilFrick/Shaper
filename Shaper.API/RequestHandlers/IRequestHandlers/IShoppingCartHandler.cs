using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers.IRequestHandlers
{
    public interface IShoppingCartHandler
    {
        Task<ShoppingCart> GetUserShoppingCartAsync(ShaperUser user);
        Task CheckOutShoppingCart(ShoppingCart userShoppingCart);
    }
}