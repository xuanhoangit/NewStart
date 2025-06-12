

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class ColorRepository :Repository<Color> ,IColorRepository
{
    public ColorRepository(IVYDbContext db):base(db)
    {
        
    }

}