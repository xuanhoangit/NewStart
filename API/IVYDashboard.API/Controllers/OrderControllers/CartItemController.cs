

using System.Security.Claims;
using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.Order;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.Controllers.OrderControllers
{
    public class CartItemController : BaseController
    {
        private readonly ICartItemService _cis;

        public CartItemController(ICartItemService cis)
        {
            _cis = cis;
        }
        [HttpGet("mycart")]
        public async Task<IActionResult> MyCart()
        {
            var httpContext = HttpContext;
            var user = httpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                return Unauthorized();
            }
            var user_id = user.FindFirstValue(ClaimTypes.NameIdentifier); // hoặc "sub"
                                                                          // var email = user.FindFirstValue(ClaimTypes.Email);
            var result = await _cis.GetUserCart(user_id);
            return GetStatusReturn(result);
        }
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO cartDTO)
        {
            var result =await _cis.AddItem(cartDTO);
            return GetStatusReturn(result);
        }
        [HttpPost("update-cartitem")]
        public async Task<IActionResult> Update([FromBody] UpdateCartItemDTO cartDTO)
        {
            var result =await _cis.UpdateCartItem(cartDTO);
            return GetStatusReturn(result);
        }
    }
}