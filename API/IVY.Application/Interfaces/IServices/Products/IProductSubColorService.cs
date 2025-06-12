using IVY.Application.DTOs;
using IVY.Domain.Models.Products;

namespace IVY.Application.Interfaces.IServices.Product;

public interface IProductSubColorService
{
    ProductSubColorGetDTO GetDTO(int psc_Id);
    Task<Result<ProductSubColorGetHomeShowDTO>> AddProductSubColor(ProductSubColorFormAddDTO productSubColorDTO);
    Result<ProductSubColorGetHomeShowDTO> UpdateProductSubColor(ProductSubColorGetDTO productSubColorDTO);
    bool Remove(int psc_Id);
}