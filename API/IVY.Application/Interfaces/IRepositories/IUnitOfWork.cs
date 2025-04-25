
using IVY.Application.Interfaces.IRepository.Products;
using IVY.Application.Interfaces.Users;

namespace IVY.Application.Interfaces.IRepository;
public interface IUnitOfWork
{   
    //VNpay
    IVnpay Vnpay{get;}
    //IOrder
    // ICartItemRepository CartItem { get; }
    // IOrderRepository Order { get; }
    // IOrderItemRepository OrderItem { get; }
    // //IProduct
    IProductColorFileRepository ProductColorFile { get; }
    IProductFavoriteRepository ProductFavorite { get; }
    IProductRepository Product { get; }
    IProductCollectionRepository ProductCollection { get; }
    ISizeRepository Size { get; }
    ICategoryRepository Category { get; }
    IColorRepository Color { get; }
    IProductColorRepository ProductColor { get; }
    IProductCategoryRepository ProductCategory { get; }

    ICustomerRepository Customer { get; }
    // IStaffInfoRepository StaffInfo { get; }
    // IAddressRepository Address { get; }
    void Save();
}