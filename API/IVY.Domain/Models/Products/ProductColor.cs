using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;


namespace IVY.Domain.Models;

public class ProductColor
{
    [Key]
    public int ProductColor__Id { get; set; }
    /// <summary>
    /// Giá bán ra
    /// </summary>
    public required decimal ProductColor__Price { get; set; }
    /// <summary>
    /// Giá xuất xưởng
    /// </summary>     
    public required decimal ProductColor__FactoryGatePrice { get; set; }
    /// <summary>
    /// Giảm giá
    /// </summary>
    public required byte ProductColor__Discount { get; set; }
    /// <summary>
    /// Màu sản phẩm ví dụ: Họa tiết đen
    /// </summary>
    public required string ProductColor__Name { get; set; }
    public DateTime ProductColor__CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime ProductColor__UpdateAt { get; set; }
    public required byte ProductColor__Status { get; set; } = (int)ProductStatus.Releasing;
    public required int ProductColor__ColorId { get; set; }
    [ForeignKey("ProductColor__ColorId")]
    public virtual Color? Color { get; set; }
    public required int ProductColor__ProductId { get; set; }
    [ForeignKey("ProductColor__ProductId")]
    public virtual Product? Product { get; set; }

}