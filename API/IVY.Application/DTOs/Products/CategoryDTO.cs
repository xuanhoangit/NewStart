namespace IVY.Application.DTOs;
public class CategoryGetDTO
{
    public int Category__Id { get; set; }
    public string? Category__Name { get; set; }
    public List<SubCategoryGetDTO>? SubCategories { get; set; }
}