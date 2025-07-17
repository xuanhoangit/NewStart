


using IVY.Application.DTOs.Filters;
using IVY.Domain.Models.Orders;

namespace IVY.Application.Interfaces.IRepository.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetOrderFiltered(OrderFilter filter);
        // Task<List<GetRevenueByDayDto>> GetRevenueDaily(DateTime time);
        // Task<List<GetRevenueByMonthDto>> GetRevenueMonthly(int month,int year);
        Task<decimal> GetUserSpend(RangeDateTime rangeDateTime,int accountId);
    }
}