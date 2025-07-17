

using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Models.Orders;

namespace IVY.Application.Interfaces.IRepository.Orders
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<List<GetCartItemDTO>> GetCartItem(string user_id);
    }
}