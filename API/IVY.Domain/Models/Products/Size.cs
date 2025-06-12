using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace IVY.Domain.Models.Products;
public class Size
{
    [Key]
    public int Size__Id { get; set; }
    public int Size__S { get; set; }
    public int Size__M { get; set; }
    public int Size__L { get; set; }
    public int Size__XL { get; set; }
    public int Size__XXl { get; set; }
    public int Size__ProductSubColorId { get; set; }
    [ForeignKey("Size__ProductSubColorId")]
    [JsonIgnore]
    public ProductSubColor? ProductSubColor { get; set; }
}