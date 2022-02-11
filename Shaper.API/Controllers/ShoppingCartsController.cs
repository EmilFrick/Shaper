using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaper.DataAccess.Repo.IRepo;
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

        [HttpGet]
        public async Task<IActionResult> AddItemToCart(CartProductAddModel cartproduct)
        {
            AddItemToCart(cartproduct);
            return Ok();
        }
    }
}
