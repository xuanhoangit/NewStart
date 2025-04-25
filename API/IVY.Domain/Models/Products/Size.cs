using System.ComponentModel.DataAnnotations;
namespace IVY.Domain.Models;
public class Size
{
    [Key]
    public int Size__Id { get; set; }
    public int Size__S { get; set; }
    public int Size__M { get; set; }
    public int Size__L { get; set; }
    public int Size__XL { get; set; }
    public int Size__XXl { get; set; }
    public int Size__ProductColorId { get; set; }
    public ProductColor? ProductColor { get; set; }
}