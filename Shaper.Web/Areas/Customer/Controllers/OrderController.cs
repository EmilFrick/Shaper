using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaper.Models.Entities;
using Shaper.Models.Models.OrderModels;
using Shaper.Web.Areas.Customer.Services.IServices;
using System.Data;

namespace Shaper.Web.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
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
            OrderEntryVM orderEntryVM = new();
            List<OrderEntryVM> orderEntriesVM = new List<OrderEntryVM>();
            foreach (var orderEntry in orders)
            {
                orderEntryVM = new OrderEntryVM(orderEntry);
                orderEntriesVM.Add(orderEntryVM);
            }

            return View(orderEntriesVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderById(new OrdersRequestModel { Identity = User.Identity.Name, OrderId = id }, HttpContext.Session.GetString("JwRoken"));
            var orderVM = new OrderDisplayVM(order);
            return View(orderVM);
        }
    }
}
