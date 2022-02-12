using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;

namespace Shaper.API.RequestHandlers
{
    public class RequestHandler : IRequestHandler
    {

        public IShaperUserHandler ShaperUsers {get; private set;}

        public IOrderHandler Orders { get; private set; }

        public IShoppingCartHandler ShoppingCarts { get; private set;}

        public RequestHandler(IUnitOfWork db)
        {
            ShaperUsers = new ShaperUserHandler(db);
            Orders = new OrderHandler(db);
            ShoppingCarts = new ShoppingCartHandler(db);
        }
    }
}
