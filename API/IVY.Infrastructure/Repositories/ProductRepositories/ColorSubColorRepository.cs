

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ColorSubColorRepository :Repository<ColorSubColor> ,IColorSubColorRepository
{
    public ColorSubColorRepository(IVYDbContext db):base(db)
    {
        
    }

}