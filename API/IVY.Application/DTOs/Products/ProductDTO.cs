using IVY.Domain.Enums;
using IVY.Domain.Models;

namespace IVY.Application.DTOs;

public class ProductFormAddDTO
{
    public required string Product__Name { get; set; }
    public required int[] CollectionIds { get; set; }
    public required int[] SubCategoryIds { get; set; }
    public required byte Product__SeasonId { get; set; }
}
public class ProductGetDTO
{
    public int Product__Id { get; set; }
    public int Product__Sold { get; set; }
    public string Product__Name { get; set; }
    public int Product__Status { get; set; } 
}
public class ProductGetWithProductHomeShowDTO
{   
    // public string? Collection__Name{ get; set; }
    public int Product__Sold { get; set; }
    public int? Product__Id { get; set; }
    public string? Product__Name { get; set; }
    public int? Product__Status { get; set; } = (int)ProductStatus.Releasing;
    // public string? Product__SeasonName { get; set; }
    public DateTime? Product__CreateAt { get; set; } = DateTime.UtcNow;
    public List<ProductSubColorGetHomeShowDTO>? ProductSubColorGetHomeShowDTOs { get; set; } = new List<ProductSubColorGetHomeShowDTO>();
}