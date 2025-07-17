using AutoMapper;
using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Order;
using IVY.Domain.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IVY.Application.Services.Order
{   

    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CartItemService(IUnitOfWork uow, IMapper mapper) 
        {
            _uow = uow;
            _mapper = mapper;
        }

        // [HttpGet("user-cart")]
        public async Task<Result<List<GetCartItemDTO>>> GetUserCart(string user_id)
        {
                var cartItems = await _uow.CartItem.GetCartItem(user_id);
                return Result<List<GetCartItemDTO>>.Success(cartItems);
        }

        // [HttpPost("add")]
        public async Task<Result<List<GetCartItemDTO>>> AddItem(AddCartDTO cartDTO)
        {
            var existingItem = _uow.CartItem.FirstOrDefault(x =>
                x.CartItem__CreatedByCustomerId == Guid.Parse(cartDTO.User__Id) &&
                x.CartItem__Size == cartDTO.Size__Name);

            if (existingItem != null)
            {
                existingItem.CartItem__Quantity += cartDTO.CartItem__Quantity;
                if (_uow.CartItem.Update(existingItem))
                {
                    var cartItems = await _uow.CartItem.GetCartItem(cartDTO.User__Id.ToString());
                    return Result<List<GetCartItemDTO>>.Success( cartItems);
                }
            }
            else
            {
                var newItem = new CartItem
                {
                    CartItem__CreatedByCustomerId = Guid.Parse(cartDTO.User__Id),
                    CartItem__Size = cartDTO.Size__Name,
                    CartItem__ProductSubColorId = cartDTO.ProductSubColor__Id,
                    CartItem__Quantity = cartDTO.CartItem__Quantity,
                }
                ;
                if (_uow.CartItem.Add(newItem))
                {
                    var cartItems = await _uow.CartItem.GetCartItem(cartDTO.User__Id);
                    return Result<List<GetCartItemDTO>>.Success(
                cartItems
                );
                }
            }
            return Result<List<GetCartItemDTO>>.Failure(ResultStatus.BadRequest); 
        }
        // [HttpPatch("item/{item_id}/quantity/{quantity}")]
        public async Task<Result<List<GetCartItemDTO>>> UpdateCartItem(UpdateCartItemDTO cartDto){
           
                var item = _uow.CartItem.Get(cartDto.CartItem__Id);
                item.CartItem__Quantity = cartDto.CartItem__Quantity;
                if (item.CartItem__Quantity <= 0)
                {
                    if (_uow.CartItem.Remove(item))
                    {
                        var cartItems = await _uow.CartItem.GetCartItem(item.CartItem__CreatedByCustomerId.ToString());
                            return Result<List<GetCartItemDTO>>.Success(
                        cartItems
                        );
                    } 
                }
                if (_uow.CartItem.Update(item))
                {
                    var cartItems = await _uow.CartItem.GetCartItem(item.CartItem__CreatedByCustomerId.ToString());
                        return Result<List<GetCartItemDTO>>.Success(
                    cartItems
                    );
                }
                return Result<List<GetCartItemDTO>>.Failure(ResultStatus.BadRequest);
            
          
        }
        // [HttpDelete("{id}")]
        // public IActionResult DeleteItem(int id)
        // {
        //     try
        //     {
        //         var currentAccount = CurrentUser() as CurrentUser;
        //         if (currentAccount == null)
        //             return Unauthorized("User not authenticated.");

        //         var item = _uow.CartItem.Get(id);
        //         if (item == null || item.CartItem__CreatedByAccountId != currentAccount.AccountId)
        //             return NotFound("Item not found or unauthorized.");

        //         if (_uow.CartItem.Remove(item))
        //             return Ok("Item removed successfully.");

        //         return BadRequest("Failed to remove item.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"An error occurred: {ex.Message}");
        //     }
        // }

        // [HttpDelete]
        // public IActionResult DeleteItems([FromBody] List<int> cartItemIds)
        // {
        //     try
        //     {
        //         var currentAccount = CurrentUser() as CurrentUser;
        //         if (currentAccount == null)
        //             return Unauthorized("User not authenticated.");

        //         if (cartItemIds == null || !cartItemIds.Any())
        //             return BadRequest("No items provided for deletion.");

        //         var items = _uow.CartItem.Find(x =>
        //             cartItemIds.Contains(x.CartItem__Id) &&
        //             x.CartItem__CreatedByAccountId == currentAccount.AccountId).ToList();

        //         if (!items.Any())
        //             return NotFound("No matching items found.");

        //         if (_uow.CartItem.RemoveRange(items))
        //             return Ok("Items removed successfully.");

        //         return BadRequest("Failed to remove items.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"An error occurred: {ex.Message}");
        //     }
        // }
    }
}
