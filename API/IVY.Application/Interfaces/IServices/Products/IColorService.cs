using IVY.Application.DTOs;
using IVY.Domain.Models.Products;

namespace IVY.Application.Interfaces.IServices.Product;
public interface IColorService {
    Task<List<ColorGetDTO>> GetAllColor();
}