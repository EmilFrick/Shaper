using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers
{
    public class ShoppingCartHandler : IShoppingCartHandler
    {

        private readonly IUnitOfWork _db;

        public ShoppingCartHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task CheckOutShoppingCart(ShoppingCart userShoppingCart)
        {
            userShoppingCart.CheckedOut = true;
            _db.ShoppingCarts.Update(userShoppingCart);
            await _db.SaveAsync();
        }

        public async Task<ShoppingCart> GetUserShoppingCartAsync(ShaperUser user)
        {
            return await _db.ShoppingCarts.GetDetailedShoppingCart(user.IdentityId);
        }
    }
}
