using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IVY.Domain.Models;
using IVY.Domain.Models.Users;
public enum PaymentStatus
{
    Unpaid=0,
    Paid,
    Refunding,
    Refunded 
}
public enum PaymentMethod
{
    Cash_on_Delivery=0,
    Prepay 
}
public static class Form_of_purchase
{
    public const string Online = "Online";
    public const string Offline = "Offline";
}
//Nếu mua tại cửa hàng thì
//---> 1) Staff tạo order => Phương thức thanh toán sẽ là Cash on Delivered 
// Nếu yêu cầu chuyển khoản thì show mã QR và VNPay xử lý khi thanh toán xong auto Confirm order
// Nếu tiền mặt thì Staff sẽ là người Confirm order
//-----------------------------------
//Nếu mua online
//---> 2) Customer tạo order => có 2 phương thức thanh toán Prepay và Cash on Delivered
namespace IVY.Domain.Models.Orders
{   
    public class Order
    {   
        [Key]
        public int Order__Id { get; set; }
        public Guid Order__CreatedByCustomerId { get; set; }//FK
        [ForeignKey("Order__CreatedByCustomerId")]
        public Customer? Customer { get; set; }
        public int Order__Status { get; set; }
        public int Order__PaymentStatus { get; set; }
        public decimal Order__AmountDue { get; set; }//Số tiền phải trả
        public long Order__PaymentCode { get; set; }
        public DateTime Order__CreatedDate { get; set; }
        public int Order__PaymentMethod {get;set;}= (int)PaymentMethod.Cash_on_Delivery;
        // public string? Order__Type {get;set;}//Mua on hay mua off
        public virtual List<OrderItem>? OrderItems {get;set;}
        // public int Order__AddressId { get; set; }
        // [ForeignKey("Order__AddressId")]
        public string Address { get; set; }
        public string? OrderCode { get; set; }
        
    }
}
