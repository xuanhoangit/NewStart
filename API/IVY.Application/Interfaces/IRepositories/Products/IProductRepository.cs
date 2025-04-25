

using IVY.Application.DTOs.Products;
using IVY.Domain.Models;

namespace IVY.Application.Interfaces.IRepository.Products;
public interface IProductRepository : IRepository<Product>
{

        // IQueryable<Product> GetFilteredProducts(ProductFilter filter);
        Task<ProductGetWithProductHomeShowDTO> GetDTO(ProductFilter filter);
        Task<List<ProductGetWithProductHomeShowDTO>> GetAllDTO(ProductFilter filter);
}