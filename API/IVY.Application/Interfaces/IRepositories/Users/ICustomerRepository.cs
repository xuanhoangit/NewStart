

using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Models.Users;

namespace IVY.Application.Interfaces.Users
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        // Task< CreateOrderRequest> GetBuyer(int order_id);
    }
}