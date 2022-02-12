using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers
{
    public class OrderHandler : IOrderHandler
    {

        private readonly IUnitOfWork _db;

        public OrderHandler(IUnitOfWork db)
        {
            _db = db;
        }

        public async Task<Order> PrepareOrder(string identityID)
        {

        }
    }
}
