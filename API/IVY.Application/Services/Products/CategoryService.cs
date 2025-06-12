using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.Product;
using IVY.Domain.Enums;
using IVY.Domain.Models;

namespace IVY.Application.Services.Products;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<List<CategoryGetDTO>> GetAllAsync(){
        var categories=await _uow.Category.GetAllAsync(x=>x.Category__Status==(int)ProductStatus.Releasing,"SubCategories");
        var listCategoryDTO=new List<CategoryGetDTO>();
        foreach(var category in categories){
            listCategoryDTO.Add(new CategoryGetDTO{
                Category__Id=category.Category__Id,
                Category__Name=category.Category__Name,
                SubCategories=category.SubCategories.Select(sub=>new SubCategoryGetDTO {
                        SubCategory__Name=sub.SubCategory__Name,
                        SubCategory__Id=sub.SubCategory__Id
                    }
                ).ToList()
                  
            });
        }
        return listCategoryDTO;
    }
}