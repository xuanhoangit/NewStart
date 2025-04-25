using System.ComponentModel.DataAnnotations;

namespace IVY.Domain.Models;
public class Color
{   
    [Key]
    public int Color__Id { get; set; }
    public required string? Color__Name { get; set; }
    public virtual ICollection<ProductColor>? ProductColors { get; set; }
}