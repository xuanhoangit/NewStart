namespace IVY.Application.DTOs.Filters
{

    public class EmployeeFilter
    {

        public string? Email { get; set; }// lọc bằng tên sản phẩm
        public string? FullName { get; set; }// lọc bằng tên sản phẩm
        public int? Gender { get; set; }// lọc bằng tên sản phẩm
        public string? RoleName { get; set; }// lọc bằng tên sản phẩm
        public int Page { get; set; } = 1;
        // public string RoleRequest { get; set; }
    }
}