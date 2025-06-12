

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class SubColorRepository :Repository<SubColor> ,ISubColorRepository
{
    public SubColorRepository(IVYDbContext db):base(db)
    {
        
    }

}