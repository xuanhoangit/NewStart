using System.ComponentModel.DataAnnotations;

namespace IVY.Domain.Models.Products;
public class Color
{   
    [Key]
    public int Color__Id { get; set; }
    public required string? Color__Name { get; set; }
    public required string? Color__Image { get; set; }
    public virtual ICollection<ColorSubColor>? ColorSubColors { get; set; }
}