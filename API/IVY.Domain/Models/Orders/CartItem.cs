using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IVY.Domain.Models.Users;

namespace IVY.Domain.Models.Orders
{
    public class CartItem
    {   
        [Key]
        public int CartItem__Id { get; set; }
        [DefaultValue(1)]
        [Range(1,int.MaxValue)]
        public int CartItem__Quantity {get;set;}
        public Guid CartItem__CreatedByCustomerId { get; set; }
        [ForeignKey("CartItem__CreatedByCustomerId")]
        public Customer? Customer { get; set; }
        public int CartItem__ProductSubColorId { get; set; }
        public string CartItem__Size { get; set; }
        //Nếu ánh xạ sẽ bị lỗi bởi nếu xóa pc => xóa pcs => xóacart
    }
}