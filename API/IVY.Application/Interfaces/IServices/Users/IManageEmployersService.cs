using IVY.Application.DTOs;
using IVY.Domain.Models.Users;

public interface IManageEmployersService
{
    Task<Result<bool>> LayOffPersonnel(UserRole model);
    Task<Result<bool>> BlockUser(string userId);
    Task<Result<bool>> UnblockUser(string userId);
    Task<Result<EmployeeIdentity>> GetEmployerInfomation(EmailUser model);
    Task<Result<bool>> PositionAppointment(UserRole model);
    
}