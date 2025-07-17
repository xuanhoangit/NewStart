using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Order
{
    public interface ICartItemService
    {
        Task<Result<List<GetCartItemDTO>>> GetUserCart(string user_id);
        Task<Result<List<GetCartItemDTO>>> UpdateCartItem(UpdateCartItemDTO cartDto);
        Task<Result<List<GetCartItemDTO>>> AddItem(AddCartDTO cartDTO);
    }
}