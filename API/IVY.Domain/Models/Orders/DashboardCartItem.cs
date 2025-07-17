using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IVY.Domain.Models.Users;

namespace IVY.Domain.Models.Orders
{
    public class DashboardCartItem
    {   
        [Key]
        public int CartItem__Id { get; set; }
        [DefaultValue(1)]
        [Range(1,int.MaxValue)]
        public int CartItem__Quantity {get;set;}
        public Guid CartItem__CreatedByEmployeeId { get; set; }
        [ForeignKey("CartItem__CreatedByEmployeeId")]
        public EmployeeIdentity? EmployeeIdentity { get; set; }
        public string CartItem__Size {get;set;}
        //Nếu ánh xạ sẽ bị lỗi bởi nếu xóa pc => xóa pcs => xóacart
    }
}