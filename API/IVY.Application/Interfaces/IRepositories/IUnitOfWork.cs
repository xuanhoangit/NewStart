
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
    IProductSubColorFileRepository ProductSubColorFile { get; }
    IProductFavoriteRepository ProductFavorite { get; }
    IProductRepository Product { get; }
    IOutfitRepository Outfit { get; }
    IProductCollectionRepository ProductCollection { get; }
    ISizeRepository Size { get; }
    ICategoryRepository Category { get; }
    IColorRepository Color { get; }
    ISubColorRepository SubColor { get; }
    IColorSubColorRepository ColorSubColor { get; }

    ICollectionRepository Collection { get; }
    IProductSubColorRepository ProductSubColor { get; }
    IProductSubCategoryRepository ProductSubCategory { get; }
    ISubCategoryRepository SubCategory { get; }

    ICustomerRepository Customer { get; }
    // IStaffInfoRepository StaffInfo { get; }
    // IAddressRepository Address { get; }
    void Save();
}