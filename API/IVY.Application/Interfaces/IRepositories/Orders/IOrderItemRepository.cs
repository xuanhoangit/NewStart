


using IVY.Application.DTOs;

using IVY.Domain.Models.GHN;
using IVY.Domain.Models.Orders;

namespace IVY.Application.Interfaces.IRepository.Orders
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<List<GetOrderItemDTO>> GetOrderItems(int order_id);
        Task<List<ItemDto>> GetItemDTO(int order_id);
        Task<GetOrderItemDTO> GetOrderItem(int orderItem_id);
    }
}