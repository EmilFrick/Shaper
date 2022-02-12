using Shaper.Models.Entities;

namespace Shaper.API.RequestHandlers
{
    public interface IOrderHandler
    {
        Task<Order> PrepareOrder(string identityID);
    }
}