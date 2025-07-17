using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IVY.Domain.Models.Users;

namespace IVY.Domain.Models.Orders
{   
    public class DashboardOrder
    {   
        [Key]
        public int Order__Id { get; set; }
        public Guid Order__CreatedByEmployeeId { get; set; }//FK
        [ForeignKey("Order__CreatedByEmployeeId")]
        public EmployeeIdentity? EmployeeIdentity { get; set; }
        public int Order__Status { get; set; }
        public int Order__PaymentStatus { get; set; }
        public decimal Order__AmountDue { get; set; }//Số tiền phải trả
        public long Order__PaymentCode { get; set; }
        public DateTime Order__CreatedDate { get; set; }
        // public int Order__PaymentMethod { get; set; } = (int)PaymentMethod.Cash_on_Delivery;
        public virtual List<OrderItem>? OrderItems {get;set;}
        // public string? OrderCode { get; set; }
        
    }
}
