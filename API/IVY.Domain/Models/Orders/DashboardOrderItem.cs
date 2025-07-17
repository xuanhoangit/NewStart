using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVY.Domain.Models.Orders
{
    public class DashboardOrderItem
    {   
        [Key]
        public int OrderItem__Id { get; set; }
        public int OrderItem__OrderId { get; set; }//FK
        [ForeignKey("OrderItem__OrderId")]
        public DashboardOrder? Order { get; set; }
        public int OrderItem__ProductSubColorId { get; set; }
        public string OrderItem__Size { get; set; }
        public int OrderItem__Quantity { get; set; }
    }
}
