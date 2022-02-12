using Shaper.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaper.DataAccess.Repo.IRepo
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task AddRangeAsync(IEnumerable<OrderDetail> creatingOrderDetails);
    }
}
