using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IVY.Domain.Models.Products;

namespace IVY.Domain.Models.Orders
{
    public class OrderItem
    {   
        [Key]
        public int OrderItem__Id { get; set; }
        public int OrderItem__OrderId { get; set; }//FK
        [ForeignKey("OrderItem__OrderId")]
        public Order? Order { get; set; }
        public string OrderItem__Size { get; set; }
        public int OrderItem__ProductSubColorId { get; set; }
        [ForeignKey("OrderItem__ProductSubColorId")]
        public ProductSubColor? ProductSubColor { get; set; }
        public int OrderItem__Quantity { get; set; }
    }
}
