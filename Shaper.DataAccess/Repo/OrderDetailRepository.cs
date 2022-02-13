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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {

        private readonly AppDbContext _db;
        public OrderDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddRangeAsync(IEnumerable<OrderDetail> items)
        {
            await _db.AddRangeAsync(items);
        }

    }
}
