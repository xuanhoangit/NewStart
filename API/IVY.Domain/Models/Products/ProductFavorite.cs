using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models.Products;

public class ProductFavorite
{
    [Key]
    public int ProductFavorite__Id { get; set; }
    public required int ProductFavorite__ProductId { get; set; }
    [ForeignKey("ProductFavorite__ProductId")]
    public Product? Product { get; set; }
}