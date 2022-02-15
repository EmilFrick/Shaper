using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;
using Shaper.Web.Areas.Customer.Services.IServices;

namespace Shaper.Web.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersByUserAsync(User.Identity.Name, HttpContext.Session.GetString("JwToken"));
            var orderDisplayVMs = OrderDisplayVM.GetOrderDisplayVMs(orders);
            return View(orderDisplayVMs);
        }
    }
}
