using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;


namespace IVY.Application.Interfaces.IServices.Product;

public interface IProductService
{
    Task<dynamic> GetProductByFilter(ProductFilter filter);
    Task<dynamic> GetProducts(int page);
    Task<Result<ProductGetWithProductHomeShowDTO>>? Add(ProductFormAddDTO productFormAddDTO);
    List<dynamic> Search(string text);
    bool Remove(int product__Id);
}