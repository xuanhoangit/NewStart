

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductSubCategoryRepository :Repository<ProductSubCategory> ,IProductSubCategoryRepository
{
    public ProductSubCategoryRepository(IVYDbContext db):base(db)
    {
        
    }

}