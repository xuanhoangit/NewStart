using IVY.Domain.Models.GHN;
namespace IVY.Application.Interfaces.IRepository;
public interface IGHN
{   
    Task<GHNResponse> CreateShippingOrderAsync(CreateOrderRequest res);
    Task<GHNResponse> GetOrderDetail(string clientOrderCode);
}