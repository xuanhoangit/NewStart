

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class CategoryRepository :Repository<Category> ,ICategoryRepository
{
    public CategoryRepository(IVYDbContext db):base(db)
    {
        
    }

}