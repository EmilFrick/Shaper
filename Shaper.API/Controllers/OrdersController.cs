using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.API.RequestHandlers.IRequestHandlers;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.Models.OrderModels;

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


        [HttpPost]
        public async Task<IActionResult> GetUserOrders(OrdersRequestModel user)
        {
            var orders = await _requestHandler.Orders.GetUserOrders(user.Identity);
            if (orders is null)
            {
                return NotFound(new { message = "This user does not have any Orders with us." });
            }
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserOrder(int id, OrdersRequestModel orderreq)
        {
            if(id == 0 || id != orderreq.OrderId)
                return BadRequest();

            var order = await _requestHandler.Orders.GetOrder(orderreq.OrderId.GetValueOrDefault());
            
            if (order is null)
            {
                return NotFound(new { message = "The order that you're trying retrieve is not with us." });
            }
            return Ok(order);
        }


        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlacingOrder(OrdersRequestModel user)
        {
            var userShoppingCart = await _requestHandler.ShoppingCarts.GetUserShoppingCartAsync(user.Identity);
            if (userShoppingCart is null || userShoppingCart.CartProducts.Count is 0)
            {
                return NotFound(new { message = "User does not have a shopping cart to process." });
            }
            var order = await _requestHandler.Orders.InitateOrderAsync(user.Identity);
            await _requestHandler.Orders.CheckOutCartProducts(userShoppingCart, order);
            await _requestHandler.Orders.ReconciliatingOrder(order.Id);
            await _requestHandler.ShoppingCarts.CheckOutShoppingCartAsync(user.Identity);
            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> PutUserOrders(OrderUpdateModel order)
        {
            if (ModelState.IsValid)
            {
                var originalOrder = await _requestHandler.Orders.GetOrder(order.OrderId);
                if (originalOrder is null)
                    return NotFound(new { message = $"Order with ID: {order.OrderId} does not exist." });

                await _requestHandler.Orders.UpdateOrder(order, originalOrder);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{orderid:int}")]
        public async Task<IActionResult> DeleteOrder(int orderid)
        {
            if (ModelState.IsValid)
            {
                var order = await _requestHandler.Orders.GetOrder(orderid);
                if (order is null)
                    return NotFound(new { message = $"We cannot find the with Order ID: {orderid}." });

                await _requestHandler.Orders.DeleteOrder(order);

                return Ok(new { message = "Entry was deleted successfully." });
            }
            return BadRequest();
        }
    }
}
