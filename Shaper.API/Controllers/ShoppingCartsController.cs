using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ShoppingCartVM;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public ShoppingCartsController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartProductAddModel cartProductModel)
        {
            if (ModelState.IsValid)
            {
                //Getting user or Creating a new Shaper User.
                var user = await _db.ShaperUsers.GetCustomerAsync(cartProductModel.ShaperUserDetails);
                
                //Getting Product details(for pricing)
                var productDetails = await _db.Products.GetFirstOrDefaultAsync(x=> x.Id == cartProductModel.ProductId);
                if (productDetails == null)
                {
                    return BadRequest();
                }
                
                //Getting current or new shopping cart.
                var userShoppingCart = await _db.ShoppingCarts.GetShoppingCartAsync(user, productDetails);
                
                //Getting possible CartProduct. if null, create new entry. If there is an entry, update the entry.
                var cartProduct = userShoppingCart.CartProducts.FirstOrDefault(x => x.ProductId == cartProductModel.ProductId);
                if (cartProduct is not null)
                {
                    cartProduct.ProductQuantity = cartProductModel.ProductQuantity;
                    _db.CartProducts.Update(cartProduct);
                    await _db.SaveAsync();
                }
                else
                {
                    cartProduct = new()
                    {
                        ProductId = cartProductModel.ProductId,
                        ProductQuantity = cartProductModel.ProductQuantity,
                        ShoppingCartId = userShoppingCart.Id,
                        UnitPrice = productDetails.Price
                    };
                    await _db.CartProducts.AddAsync(cartProduct);
                    await _db.SaveAsync();
                }

                _db.ShoppingCarts.CalulatingShoppingCartValue(userShoppingCart);
                return Ok(cartProduct);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
