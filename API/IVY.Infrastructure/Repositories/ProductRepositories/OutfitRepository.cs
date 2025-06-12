using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class OutfitRepository :Repository<Outfit>, IOutfitRepository
{
    public OutfitRepository(IVYDbContext db):base(db)
    {
        
    }
}