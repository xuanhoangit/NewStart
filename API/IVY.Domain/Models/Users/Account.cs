using IVY.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IVY.Domain.Models.Users;

public class EmployeeIdentity : IdentityUser<Guid>
{   
    public DateTime DateOfBirth { get; set; }
    public int Gender { get; set; }
    public string Department { get; set; }
    public DateTime CreateDate { get; set; }
}
public class Customer
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string PhoneNumberConfirmed { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Gender { get; set; }
    public string Roles = RolesName.Customer;
    // Thêm các thông tin khác
}