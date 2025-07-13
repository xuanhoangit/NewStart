using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Domain.Models.Users;
using static IVY.Application.DTOs.User;

public interface IEmployeeManagerService
{
    Task<Result<UserWithRoleDTO>> UpdateProfile(RegisterDto model);
    Task<Result<List<UserWithRoleDTO>>> GetEmployeeNoRole();
    Task<Result<List<UserWithRoleDTO>>> GetEmployees(EmployeeFilter filter);
    Task<Result<bool>> RevokeAccess(UserRole model);
    Task<Result<bool>> BlockUser(string userId);
    Task<Result<bool>> UnblockUser(string userId);
    Task<Result<EmployeeIdentity>> GetEmployerInfomation(EmailUser model);
    Task<Result<bool>> PositionAppointment(UserRole model);
    
}