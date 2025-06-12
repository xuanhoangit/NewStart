

using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

using IVY.Application.Interfaces.IRepository.Products;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductSubColorRepository :Repository<ProductSubColor> ,IProductSubColorRepository
{
    public ProductSubColorRepository(IVYDbContext db):base(db)
    {
        
    }

}