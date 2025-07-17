using IVY.Domain.Enums;
using IVY.Domain.Models;
using IVY.Domain.Models.Products;

namespace IVY.Application.DTOs;

public class ProductSubColorFormAddDTO
{   
    public required decimal ProductSubColor__Price { get; set; }
    public required byte ProductSubColor__Discount { get; set; }
    public required int ProductSubColor__ProductId { get; set; }
    public required int ProductSubColor__SubColorId { get; set; }
}
public class ProductSubColorFormUpdateDTO
{
    public int ProductSubColor__Id { get; set; }
    public required decimal ProductSubColor__Price { get; set; }
    public required byte ProductSubColor__Discount { get; set; }
    public required int ProductSubColor__ProductId { get; set; }
    public required int ProductSubColor__SubColorId { get; set; }
    
    public required string ProductSubColor__OutfitKey { get; set; }
    public required int ProductSubColor__Status { get; set; }
}
public class ProductSubColorGetDTO
{   
    public int? ProductSubColor__Id { get; set; }
    public  decimal? ProductSubColor__Price { get; set; }
    public  byte? ProductSubColor__Discount { get; set; }
    public  int? ProductSubColor__ProductId { get; set; }
    public  int? ProductSubColor__SubColorId { get; set; }
    public  string? ProductSubColor__OutfitKey { get; set; }
    public  byte? ProductSubColor__Status { get; set; }
    
    public DateTime ProductSubColor__CreateAt { get; set; }
    public DateTime ProductSubColor__UpdateAt { get; set; } = DateTime.UtcNow;
    public SubColorGetDTO? SubColorGetDTO { get; set; }
}
public class ProductSubColorGetHomeShowDTO
{
     public int ProductSubColor__Id { get; set; }
    public required decimal ProductSubColor__Price { get; set; }
    public required byte ProductSubColor__Discount { get; set; }
    public required int ProductSubColor__ProductId { get; set; }
    public required int ProductSubColor__SubColorId { get; set; }
    public required string ProductSubColor__OutfitKey { get; set; }
    public required byte ProductSubColor__Status { get; set; }
    
    public DateTime ProductSubColor__CreateAt { get; set; }
    public DateTime ProductSubColor__UpdateAt { get; set; } = DateTime.UtcNow;
    public List<ProductSubColorFileGetFileDTO>? ProductSubColorFileGetFileDTOs { get; set; } = new List<ProductSubColorFileGetFileDTO>();
    public SubColorGetDTO? SubColorGetDTO{ get; set; }
    public SizeDTO? SizeDTO{ get; set; }
}