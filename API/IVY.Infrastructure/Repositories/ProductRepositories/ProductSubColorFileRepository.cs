

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductSubColorFileRepository :Repository<ProductSubColorFile> ,IProductSubColorFileRepository
{
    public ProductSubColorFileRepository(IVYDbContext db):base(db)
    {
        
    }

}