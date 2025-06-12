using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace IVY.Domain.Models.Products
{
    public class ProductSubColorFile
    {   
        [Key]
        public int ProductSubColorFile__Id { get; set; }
        public required string ProductSubColorFile__Name { get; set; }
        public required byte ProductSubColorFile__Index { get; set; }
        public int ProductSubColorFile__ProductSubColorId { get; set; }
        [ForeignKey("ProductSubColorFile__ProductSubColorId")]
        public ProductSubColor? ProductSubColor { get; set; }
    }
}