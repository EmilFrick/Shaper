namespace Shaper.Web.Areas.Customer.Services.IServices
{
    public interface IShoppingCartService
    {
        Task AddProductToUsersCartAsync(int id, int quantity, string user, string token);
    }
}
