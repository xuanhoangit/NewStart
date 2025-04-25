using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace IVY.Domain.Models
{
    public class ProductColorFile
    {   
        [Key]
        public int ProductFile__Id { get; set; }
        public required string ProductFile__Name { get; set; }=Guid.NewGuid().ToString()+".jpg";
        public required int ProductColor__Id { get; set; }
        [ForeignKey("ProductColor__Id")]
        public ProductColor? ProductColor { get; set; }
    }
}