using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.ViewModels.Order;
using Shaper.Models.ViewModels.ShoppingCartVM;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRequestHandler _requestHandler;

        public OrdersController(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders(string user)
        {
            var orders = await _requestHandler.Orders.GetUserOrders(user);
            if (orders is null)
            {
                return NotFound();
            }
            return Ok(orders);
        }


        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlacingOrder(string identity)
        {
            var userShoppingCart = await _requestHandler.ShoppingCarts.GetUserShoppingCartAsync(identity);
            if (userShoppingCart is null || userShoppingCart.CartProducts.Count is 0)
            {
                return BadRequest();
            }
            var order = await _requestHandler.Orders.InitateOrderAsync(identity);
            await _requestHandler.Orders.CheckOutCartProducts(userShoppingCart, order);
            await _requestHandler.Orders.ReconciliatingOrder(order.Id);
            await _requestHandler.ShoppingCarts.CheckOutShoppingCart(identity);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> PutUserOrders(OrderUpdateModel order)
        {
            if (ModelState.IsValid)
            {

                var originalOrder = await _requestHandler.Orders.GetOrder(order.OrderId);
                if (originalOrder is null)
                    return NotFound();

                await _requestHandler.Orders.UpdateOrder(order, originalOrder);
                return Ok();
            }
            return BadRequest();
        }
    }
}