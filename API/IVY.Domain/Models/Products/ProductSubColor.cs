using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IVY.Domain.Enums;


namespace IVY.Domain.Models.Products;

public class ProductSubColor
{
    [Key]
    public int ProductSubColor__Id { get; set; }
    public required decimal ProductSubColor__Price { get; set; }
    public required string ProductSubColor__OutfitKey { get; set; } 
    public required byte ProductSubColor__Discount { get; set; }
    public DateTime ProductSubColor__CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime ProductSubColor__UpdateAt { get; set; }
    public required byte ProductSubColor__Status { get; set; } = (int)ProductStatus.Releasing;
    public required int ProductSubColor__SubColorId { get; set; }
    [ForeignKey("ProductSubColor__SubColorId")]
    public virtual SubColor? SubColor { get; set; }
    public required int ProductSubColor__ProductId { get; set; }
    [ForeignKey("ProductSubColor__ProductId")]
    public virtual Product? Product { get; set; }
    public virtual Size? Size { get; set; }
    public virtual ICollection<ProductSubColorFile>? ProductSubColorFile { get; set; }

}