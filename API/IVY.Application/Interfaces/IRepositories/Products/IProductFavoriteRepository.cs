
using IVY.Domain.Models;

namespace IVY.Application.Interfaces.IRepository.Products;

    public interface IProductFavoriteRepository : IRepository<ProductFavorite>
    {
        // Task<List<GetFavoriteDTO>> GetFavorites(int account_id);
    }
