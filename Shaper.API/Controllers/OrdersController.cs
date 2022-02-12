using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
using Shaper.Models.ViewModels.ShoppingCartVM;

namespace Shaper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public OrdersController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> PlacingOrder(string identity)
        {
            
            //Get Shapercustomer ShoppingCart
            var userShoppingCart = await _db.ShoppingCarts.GetFirstOrDefaultAsync(x => x.Customer.IdentityId == identity && x.CheckedOut == false, includeProperties:"CartProducts");
            if (userShoppingCart == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
