using IVY.Domain.Models;

namespace IVY.Application.DTOs.Products;

public class ProductFormAddDTO
{
    public int Product__Id { get; set; }
    public required string Product__Name { get; set; }
    public required int Product__Status { get; set; } = (int)ProductStatus.Releasing;
    public required int Product__SeasonId { get; set; }
    public DateTime Product__CreateAt { get; set; } = DateTime.UtcNow;
}
public class ProductGetWithProductHomeShowDTO
{   
    public string? Collection__Name{ get; set; }
    public int Product__Sold { get; set; }
    public int? Product__Id { get; set; }
    public string? Product__Name { get; set; }
    public int? Product__Status { get; set; } = (int)ProductStatus.Releasing;
    public string? Product__SeasonName { get; set; }
    public DateTime? Product__CreateAt { get; set; } = DateTime.UtcNow;
    public ProductColorGetHomeShowDTO? ProductColorGetHomeShowDTO { get; set; }
}