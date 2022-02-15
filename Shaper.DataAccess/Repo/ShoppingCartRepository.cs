using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using SnutteBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }

        public async Task<ShoppingCart> GetDetailedShoppingCart(string id)
        {
            var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(y => y.CustomerIdentity == id && y.CheckedOut == false);
            var cartProductsResult = await _db.CartProducts.Include(p => p.Product)
                                                                .ThenInclude(c => c.Color)
                                                             .Include(p => p.Product)
                                                             .ThenInclude(s => s.Shape)
                                                             .Include(p => p.Product)
                                                                .ThenInclude(t => t.Transparency)
                                                             .Include(sc => sc.ShoppingCart)
                                                                 .Where(x => x.ShoppingCart.CartProducts
                                                                    .Any(y => y.ShoppingCart.CustomerIdentity == id && y.ShoppingCart.CheckedOut == false))
                                                             .ToListAsync();
            List<CartProduct> cartProducts = new List<CartProduct>();
            foreach (var item in cartProductsResult)
            {
                cartProducts.Add(item);
            }
            if(cartProductsResult.Count > 0)
                cart.CartProducts = cartProductsResult;                

            return cart;
        }
}
}
