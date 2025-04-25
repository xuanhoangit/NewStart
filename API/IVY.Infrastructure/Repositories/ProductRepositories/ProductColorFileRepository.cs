

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ProductColorFileRepository :Repository<ProductColorFile> ,IProductColorFileRepository
{
    public ProductColorFileRepository(IVYDbContext db):base(db)
    {
        
    }

}