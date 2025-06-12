
using IVY.Domain.Models.Products;

namespace IVY.Application.Interfaces.IRepository.Products;

    public interface IProductFavoriteRepository : IRepository<ProductFavorite>
    {
        // Task<List<GetFavoriteDTO>> GetFavorites(int account_id);
    }
