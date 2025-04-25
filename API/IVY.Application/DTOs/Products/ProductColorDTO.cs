using IVY.Domain.Models;

namespace IVY.Application.DTOs;

public class ProductColorFormAddDTO
{
    public required decimal ProductColor__Price { get; set; }
    public required byte ProductColor__Discount { get; set; }
    public DateTime ProductColor__CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime ProductColor__UpdateAt { get; set; }
    public string? ProductColor__Name { get; set; }
    public required int ProductColor__ProductId { get; set; }
    public required int ProductColor__ColorId { get; set; }
}
public class ProductColorGetHomeShowDTO
{
    public int? ProductColor__Id { get; set; }
    public decimal? ProductColor__Price { get; set; }
    public byte? ProductColor__Discount { get; set; }
    public DateTime? ProductColor__CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProductColor__UpdateAt { get; set; }
    public byte? ProductColor__Status { get; set; } = (int)ProductStatus.Releasing;

    public string? ProductColor__Name { get; set; }
    public string? ProductColorFile__Name { get; set; } 
    public int? ProductColor__ColorId { get; set; }
    public int? ProductColor__ProductId { get; set; }

}