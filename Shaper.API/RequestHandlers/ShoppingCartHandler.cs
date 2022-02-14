using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.Models.ShoppingCartModels;
using System.Web.Helpers;

namespace Shaper.API.RequestHandlers
{
    public class ShoppingCartHandler : IShoppingCartHandler
    {

        private readonly IUnitOfWork _db;

        public ShoppingCartHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task CheckOutShoppingCart(string user)
        {
            var userShoppingCart = await _db.ShoppingCarts.GetFirstOrDefaultAsync(a => a.CustomerIdentity == user && a.CheckedOut == false);
            userShoppingCart.CheckedOut = true;
            _db.ShoppingCarts.Update(userShoppingCart);
            await _db.SaveAsync();
        }

        public async Task<ShoppingCart> GetUserShoppingCartAsync(string user)
        {
            return await _db.ShoppingCarts.GetDetailedShoppingCart(user);
        }

        public async Task AddNewCartProductAsync(CartProductAddModel productModel, double unitprice, int shoppingcartId)
        {
            CartProduct product = new()
            {
                ProductId = productModel.ProductId,
                ProductQuantity = productModel.ProductQuantity,
                ShoppingCartId = shoppingcartId,
                UnitPrice = unitprice
            };
            await _db.CartProducts.AddAsync(product);
            await _db.SaveAsync();
        }


        public async Task UpdateCartProductAsync(CartProduct product, int quantityUpdate)
        {
            product.ProductQuantity = quantityUpdate;
            _db.CartProducts.Update(product);
            await _db.SaveAsync();
        }



        public async Task<ShoppingCart> GetFreshShoppingCartAsync(string user)
        {
            ShoppingCart cart = new()
            {
                CustomerIdentity = user,
                CheckedOut = false,
                OrderValue = 0,
            };
            await _db.ShoppingCarts.AddAsync(cart);
            await _db.SaveAsync();
            return await _db.ShoppingCarts.GetFirstOrDefaultAsync(x => x.CustomerIdentity == user && x.CheckedOut == false, includeProperties: "CartProducts");
        }

        public async Task<ShoppingCart> ShoppingCartExistAsync(string user)
        {
            return await _db.ShoppingCarts.GetFirstOrDefaultAsync(x => x.CustomerIdentity == user && x.CheckedOut == false, includeProperties: "CartProducts");
        }


        public async Task RemoveItemFromShoppingCart(int cartId, int productId)
        {
            var itemToDelete = await _db.CartProducts.GetFirstOrDefaultAsync(a => a.ShoppingCartId == cartId && a.ProductId == productId);
            _db.CartProducts.Remove(itemToDelete);
            await _db.SaveAsync();
        }

        public async Task CalulatingShoppingCartValue(ShoppingCart cart)
        {
            double cartValue = 0;
            foreach (var item in cart.CartProducts)
            {
                cartValue += (item.ProductQuantity * item.UnitPrice);
            }
            cart.OrderValue = cartValue;
            _db.ShoppingCarts.Update(cart);
            await _db.SaveAsync();
        }
    }
}
