

using IVY.Application.Interfaces.IRepository.Products;
using IVY.Domain.Models.Products;
using IVY.Infrastructure.Data;

namespace IVY.Infrastructure.Repositories.ProductRepositories;
public class CollectionRepository :Repository<Collection> ,ICollectionRepository
{
    public CollectionRepository(IVYDbContext db):base(db)
    {
        
    }

}