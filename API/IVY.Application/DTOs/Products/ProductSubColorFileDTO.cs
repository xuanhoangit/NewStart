using Microsoft.AspNetCore.Http;

namespace IVY.Application.DTOs;

public class ProductSubColorFileUpdateFileDTO
{   
    public int ProductSubColorFile__Id { get; set; }
    public byte ProductSubColorFile__Index { get; set; }
}
public class ProductSubColorFileAddFileDTO
{
    public required IFormFile FileImage { get; set; }
    public int ProductSubColorFile__ProductSubColorId { get; set; }
    public byte ProductSubColorFile__Index { get; set; }
}
public class ProductSubColorFileGetFileDTO
{   
    public byte ProductSubColorFile__Index { get; set; }

    public string? ProductSubColorFile__Name { get; set; }
    public int? ProductSubColorFile__ProductColorId { get; set; }
    public int? ProductSubColorFile__Id { get; set; }
}