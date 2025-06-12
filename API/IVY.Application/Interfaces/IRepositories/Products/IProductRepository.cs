

using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Domain.Models.Products;

namespace IVY.Application.Interfaces.IRepository.Products;
public interface IProductRepository : IRepository<Product>
{

        Task<bool> AddProduct(ProductFormAddDTO addDTO);
        // Task<ProductGetWithProductHomeShowDTO> GetDTO(ProductFilter filter);
        Task<List<ProductGetWithProductHomeShowDTO>> GetAllDTO(ProductFilter filter);
        Task<List<ProductGetWithProductHomeShowDTO>> GetAllDTO(int page);
}