using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Models.Users;
using static IVY.Application.DTOs.User;

public interface IEmployerManageRepository : IRepository<EmployeeIdentity>
{
    Task<List<UserWithRoleDTO>> GetFilteredEmployees(EmployeeFilter filter);
    Task<List<UserWithRoleDTO>> GetEmployeeNoRole();
}