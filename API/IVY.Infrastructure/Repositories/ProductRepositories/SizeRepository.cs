

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class SizeRepository :Repository<Size> ,ISizeRepository
{
    public SizeRepository(IVYDbContext db):base(db)
    {
        
    }

}