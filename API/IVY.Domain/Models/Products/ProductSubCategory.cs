using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models.Products;

public class ProductSubCategory
{
    [Key]
    public int ProductSubCategory__Id { get; set; }
    public required  int ProductSubCategory__ProductId { get; set; }
    [ForeignKey("ProductSubCategory__ProductId")]
    public Product? Product { get; set; }

    public  int ProductSubCategory__SubCategoryId { get; set; }
     [ForeignKey("ProductSubCategory__SubCategoryId")]
    public SubCategory? SubCategory { get; set; }
}