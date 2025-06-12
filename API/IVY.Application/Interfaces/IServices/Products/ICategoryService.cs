using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Product;
public interface ICategoryService {
     Task<List<CategoryGetDTO>> GetAllAsync();
}