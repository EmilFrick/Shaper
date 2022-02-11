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

        [HttpGet]
        public async Task<IActionResult> AddItemToCart(CartProductAddModel cartproduct)
        {
            var user = await _db.ShaperUsers.GetFirstOrDefaultAsync(x=>x.IdentityId == cartproduct.ShaperUserDetails.IdentityId);
            if (user is null)
            {
                user = cartproduct.ShaperUserDetails.GetEntity();
                await _db.ShaperUsers.AddAsync(user);
                
            }

            return Ok();
        }
    }
}
