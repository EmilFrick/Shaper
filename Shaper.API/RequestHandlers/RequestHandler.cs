using Shaper.API.RequestHandlers.IRequestHandlers;

namespace Shaper.API.RequestHandlers
{
    public class RequestHandler : IRequestHandler
    {

        public IShaperUserHandler ShaperUsers {get; private set;}

        public IOrderHandler Orders { get; private set; }
        public RequestHandler(IShaperUserHandler shaperUsers, IOrderHandler orders)
        {
            ShaperUsers = shaperUsers;
            Orders = orders;
        }
    }
}
