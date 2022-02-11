using Microsoft.EntityFrameworkCore;
using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using SnutteBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo
{
    public class CartProductRepository : Repository<CartProduct>, ICartProductRepository
    {
        private readonly AppDbContext _db;

        public CartProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }



        public async void Update(CartProduct cartProduct)
        {
            _db.CartProducts.Update(cartProduct);
        }
    }
}
