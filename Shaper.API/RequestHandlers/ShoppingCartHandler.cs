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

        public async Task CheckOutShoppingCartAsync(string user)
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


        public async Task RemoveItemFromShoppingCartAsync(int cartId, int productId)
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

        public async Task<ShoppingCart> GetShoppingCartByIDAsync(int id)
        {
            return await _db.ShoppingCarts.GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateShoppingCartAsync(int id, ShoppingCartUpdateModel cart)
        {
            var originalShoppingcart = await _db.ShoppingCarts.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "CartProducts");

            if (cart.CartProducts != null && cart.CartProducts.Count > 0)
            {
                List<CartProduct> cartProducts = new();
                CartProduct cartProduct = new();
                double sum = 0;
                foreach (var item in cart.CartProducts)
                {
                    var productdetails = await _db.Products.GetFirstOrDefaultAsync(x => x.Id == item.ProductId);
                    cartProduct = new()
                    {
                        ProductId = item.ProductId,
                        ProductQuantity = item.Quantity,
                        UnitPrice = productdetails.Price
                    };
                    sum += (cartProduct.UnitPrice * cartProduct.ProductQuantity);
                    cartProducts.Add(cartProduct);
                }
                originalShoppingcart.OrderValue = sum;
                originalShoppingcart.CartProducts = cartProducts;
            }

            originalShoppingcart.CustomerIdentity = cart.UserIdentity;
            originalShoppingcart.CheckedOut = cart.CheckedOut.GetValueOrDefault();
            _db.ShoppingCarts.Update(originalShoppingcart);
            await _db.SaveAsync();
        }

        public async Task<ShoppingCart> RemoveShoppingCart(int id)
        {
            var cart = await _db.ShoppingCarts.GetFirstOrDefaultAsync(x => x.Id == id);
            if(cart is null)
                return null;

            _db.ShoppingCarts.Remove(cart);
            await _db.SaveAsync();
            return cart;
        }
    }
}
