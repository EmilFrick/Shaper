using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShoppingCartVM;
using SnutteBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _db;

        public ShoppingCartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task CalulatingShoppingCartValue(ShoppingCart cart)
        {
            double cartValue = 0;
            foreach (var item in cart.CartProducts)
            {
                cartValue += (item.ProductQuantity * item.UnitPrice);
            }
            cart.OrderValue = cartValue;
            Update(cart);
            await _db.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(ShaperUser user, Product product)
        {
            var userShoppingCart = await GetFirstOrDefaultAsync(x => x.Customer.IdentityId == user.IdentityId && x.CheckedOut == false, includeProperties:"CartProducts");
            if (userShoppingCart is null)
            {
                userShoppingCart = new()
                {
                    CustomerId = user.Id,
                    CheckedOut = false,
                    OrderValue = 0,
                };
                await _db.ShoppingCarts.AddAsync(userShoppingCart);
                await _db.SaveChangesAsync();
            }
            return userShoppingCart;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }
    }
}
