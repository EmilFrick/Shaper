using Shaper.Models.Entities;
using Shaper.Models.Models.ShoppingCartModels;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Customer.Services.IServices;
using static Shaper.Utility.ApiPaths;

namespace Shaper.Web.Areas.Customer.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShaperApiService _apiService;

        public ShoppingCartService(IShaperApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task AddProductToUsersCartAsync(int id, int quantity, string user, string token)
        {
            CartProductAddModel addProduct = new()
            {
                ProductId = id,
                ProductQuantity = quantity,
                ShaperCustomer = user
            };

            _apiService.ShoppingApi.AddingProductToUserShoppingCart(addProduct, ApiPath.ShoppingCartsAddItem.GetEndpoint(), token);
        }


    }
}
