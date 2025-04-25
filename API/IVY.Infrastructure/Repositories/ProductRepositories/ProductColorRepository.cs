

using IVY.Domain.Models;
using IVY.Infrastructure.Data;

using IVY.Application.Interfaces.IRepository.Products;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductColorRepository :Repository<ProductColor> ,IProductColorRepository
{
    public ProductColorRepository(IVYDbContext db):base(db)
    {
        
    }

}