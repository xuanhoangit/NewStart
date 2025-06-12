using Microsoft.AspNetCore.Http;

namespace IVY.Application.DTOs;
public class SubColorAddDTO
{
    public required string SubColor__Name { get; set; }
    public required IFormFile SubColor__Image { get; set; }
    public required int[] ColorIds {get;set;}
}
public class SubColorGetDTO
{
    public required int SubColor__Id { get; set; }
    public required string SubColor__Name { get; set; }
    public required string SubColor__Image { get; set; }
}