using Microsoft.AspNetCore.Http;

namespace IVY.Application.DTOs;

public class ProductColorFileAddFileDTO
{
    public required IFormFile FileImage { get; set; }
    public int ProductColorFile__ProductColorId { get; set; }
}
public class ProductColorFileGetFileDTO
{
    public string? ProductColorFile__Name { get; set; }
    public int? ProductColorFile__ProductColorId { get; set; }
    public int? ProductColorFile__Id { get; set; }
}