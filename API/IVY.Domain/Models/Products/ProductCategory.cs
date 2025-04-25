using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models;

public class ProductCategory
{
    [Key]
    public int ProductCategory__Id { get; set; }
    public int ProductCategory__Status { get; set; } = (int)ProductStatus.Releasing;
    public bool ProductCategory__IsEvent { get; set; }
    public required  int ProductCategory__ProductId { get; set; }
    [ForeignKey("ProductCategory__ProductId")]
    public Product? Product { get; set; }

    public  required int ProductCategory__CategoryId { get; set; }
     [ForeignKey("ProductCategory__CategoryId")]
    public Category? Category { get; set; }
}