using Shaper.DataAccess.Context;
using Shaper.DataAccess.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        public IShapeRepository Shapes { get; private set; }
        public IColorRepository Colors { get; private set; }
        public ITransparencyRepository Transparencies { get; private set; }
        public IProductRepository Products { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public ICartProductRepository CartProducts { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
        public IShaperUserRepository ShaperUsers { get; private set; }

        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Shapes = new ShapeRepository(_db);
            Colors = new ColorRepository(_db);
            Transparencies = new TransparencyRepository(_db);
            Products = new ProductRepository(_db);
            ShoppingCarts = new ShoppingCartRepository(_db);
            Orders = new OrderRepository(_db);
            CartProducts = new CartProductRepository(_db);
            ShaperUsers = new ShaperUserRepository(_db);
        }


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
