

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductCategoryRepository :Repository<ProductCategory> ,IProductCategoryRepository
{
    public ProductCategoryRepository(IVYDbContext db):base(db)
    {
        
    }

}