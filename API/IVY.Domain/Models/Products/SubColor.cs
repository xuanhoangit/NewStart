using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models.Products;
public class SubColor
{   
    [Key]
    public int SubColor__Id { get; set; }
    /// <summary>
    /// Màu sản phẩm ví dụ: Họa tiết đen
    /// </summary>
    public required string SubColor__Name { get; set; }
    public required string SubColor__Image { get; set; }
    public required byte SubColor__Status { get; set; }
    public virtual ICollection<ProductSubColor>? ProductSubColors { get; set; }
    public virtual ICollection<ColorSubColor>? ColorSubColors { get; set; }
}