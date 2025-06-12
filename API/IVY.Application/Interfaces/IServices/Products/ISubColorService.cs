using IVY.Application.DTOs;
using IVY.Domain.Models.Products;

namespace IVY.Application.Interfaces.IServices.Product;
public interface ISubColorService{
    Task<List<SubColorGetDTO>> GetAllAsync();
    Task<Result<SubColor>>? AddSubColor(SubColorAddDTO subColorDTO);
}