using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IVY.Domain.Models.Products;
public class ColorSubColor
{   
    [Key]
    public int ColorSubColor__Id { get; set; }
    public int ColorSubColor__SubColorId { get; set; }
    [ForeignKey("ColorSubColor__SubColorId")]
    [JsonIgnore]
    public SubColor? SubColor { get; set; }
    public int ColorSubColor__ColorId { get; set; }
    [ForeignKey("ColorSubColor__ColorId")]
    public Color? Color { get; set; }
}