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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order order)
        {
            _db.Orders.Update(order);
        }

        public async Task<Order> PrepareOrder(int customerId)
        {
            Order order = new()
            {
                CustomerId =customerId,
            };
            return order;
        }
    }
}
