using IVY.Application.Interfaces;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IRepository.Orders;
using IVY.Application.Interfaces.IRepository.Products;
using IVY.Application.Interfaces.Users;
using IVY.Infrastructure.Data;
using IVY.Infrastructure.Repositories;
using IVY.Infrastructure.Repositories.ProductRepositories;
using VNPAY.NET;

namespace SneakerAPI.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly IVYDbContext _db;
    // private readonly IConfiguration _config;
    public UnitOfWork(IVYDbContext db)
    {
        _db = db;
        Vnpay = new Vnpay();
        GHN = new GHN();
        ProductSubColorFile = new ProductSubColorFileRepository(_db);
        Product =  new ProductRepository(_db);
        Outfit=new OutfitRepository(_db);
        ProductCollection =  new ProductCollectionRepository(_db);
        Size =  new SizeRepository(_db);
        Category =  new CategoryRepository(_db);
        Color =  new ColorRepository(_db);
        Collection =  new CollectionRepository(_db);
        ProductSubColor =  new ProductSubColorRepository(_db);
        SubColor =  new SubColorRepository(_db);
        ColorSubColor =  new ColorSubColorRepository(_db);
        ProductSubCategory =  new ProductSubCategoryRepository(_db);
        SubCategory =  new SubCategoryRepository(_db);
        Employee =  new EmployerManageRepository(_db);
        
        
    }
    public ICartItemRepository CartItem {get;}
    public IOrderRepository Order {get;}

    public IOrderItemRepository OrderItem {get;}

    public IProductSubColorFileRepository ProductSubColorFile {get;}
    public IProductRepository Product {get;}
    public IOutfitRepository Outfit {get;}
    public IProductFavoriteRepository ProductFavorite { get; }

    public IProductCollectionRepository ProductCollection { get; }

    public ISizeRepository Size { get; }

    public ICategoryRepository Category {get;}

    public IColorRepository Color {get;}
    public ISubColorRepository SubColor {get;}
    public IColorSubColorRepository ColorSubColor {get;}
    public ICollectionRepository Collection {get;}

    public IProductSubColorRepository ProductSubColor {get;}

    public IProductSubCategoryRepository ProductSubCategory {get;}
    public ISubCategoryRepository SubCategory {get;}
    





    public ICustomerRepository Customer {get;}

    public IEmployerManageRepository Employee {get;}

    // public IAddressRepository Address {get;}

    public IVnpay Vnpay {get;}
    public IGHN GHN {get;}



    // public IFavoriteRepository Favorite {get;}






    // public IOrderRepository Order {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}