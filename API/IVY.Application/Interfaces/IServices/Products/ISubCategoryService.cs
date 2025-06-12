using IVY.Application.DTOs;

namespace IVY.Application.Interfaces.IServices.Product;
public interface ISubCategoryService {
     Task<List<SubCategoryGetDTO>> GetAllAsync();
}