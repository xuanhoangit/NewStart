

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class SubCategoryRepository :Repository<SubCategory> ,ISubCategoryRepository
{
    public SubCategoryRepository(IVYDbContext db):base(db)
    {
        
    }

}