using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models.Products
{
    public class Outfit
    {
        [Key]
        public int Outfit__Id { get; set; }
        public required string Outfit__Key { get; set; }
        public int Outfit__ProductSubColorId { get; set; }
        [ForeignKey("Outfit__ProductSubColorId")]
        public ProductSubColor? ProductSubColor { get; set; }
    }
}