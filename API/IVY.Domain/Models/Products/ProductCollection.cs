using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models.Products;

public class ProductCollection
{
    [Key]
    public int ProductCollection__Id { get; set; }
    public required int ProductCollection__ProductId { get; set; }
    [ForeignKey("ProductCollection__ProductId")]
    public Product? Product { get; set; }
    public required  int ProductCollection__CollectionId { get; set; }
     [ForeignKey("ProductCollection__CollectionId")]
    public Collection? Collection { get; set; }
}