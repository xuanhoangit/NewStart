
using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductCollectionRepository :Repository<ProductCollection> ,IProductCollectionRepository
{
    public ProductCollectionRepository(IVYDbContext db):base(db)
    {
        
    }

}