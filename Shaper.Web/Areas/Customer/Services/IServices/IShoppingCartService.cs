using Shaper.Models.Entities;

namespace Shaper.Web.Areas.Customer.Services.IServices
{
    public interface IShoppingCartService
    {
        Task AddProductToUsersCartAsync(int id, int quantity, string user, string token);
        Task<ShoppingCart> GetUserShoppingCartAsync(string user, string token);
        Task<Order> CheckoutShoppingCartAsync(string user, string token);
    }
}
