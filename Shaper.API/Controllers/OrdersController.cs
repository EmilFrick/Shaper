using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.ViewModels.ShoppingCartVM;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly IRequestHandler _requestHandler;

        public OrdersController(IUnitOfWork db, IRequestHandler requestHandler)
        {
            _db = db;
            _requestHandler = requestHandler;
        }

        [HttpPost]
        public async Task<IActionResult> PlacingOrder(string identity)
        {
            var user = await _requestHandler.ShaperUsers.FindShaperUserByIdentityAsync(identity);
            if (user == null)
            {
                return NotFound();
            }
            var userShoppingCart = await _requestHandler.ShoppingCarts.GetUserShoppingCartAsync(user);
            if (userShoppingCart == null)
            {
                return BadRequest();
            }
            var order = await _requestHandler.Orders.InitateOrderAsync(user);
            await _requestHandler.Orders.CheckOutCartProducts(userShoppingCart, order);
            await _requestHandler.ShoppingCarts.CheckOutShoppingCart(userShoppingCart);
            return Ok();
        }
    }
}
