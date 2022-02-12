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
        public OrderDetailRepository(AppDbContext db) : base(db)
        {
        }

        public async Task AddRangeAsync(IEnumerable<OrderDetail> creatingOrderDetails)
        {
            await AddRangeAsync(creatingOrderDetails);
        }
    }
}
