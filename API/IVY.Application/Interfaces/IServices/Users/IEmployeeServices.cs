using IVY.Application.DTOs;
using IVY.Domain.Models.Users;

namespace IVY.Application.Interfaces.IServices.User;

public interface IEmployeeService
{
    Task<Result<string>> ResetPassword(ResetPasswordDto model);
    Task<bool> VerifyRecaptcha(string token);
    Task<CurrentUser>? GetCurrentUser();
    Task<Result<dynamic>> RefreshToken();
    Task<Result<EmployeeIdentity>> Register(RegisterDto model);
    // Task<Result<string>> ResendEmailConfirmation(ResendOTPDto model);
    // Task<Result<bool>> ConfirmEmail(ConfirmEmailDto model);
    Task<Result<object>> Login(LoginDto model);
    Task<Result<string>> ForgotPassword(ForgotPasswordDto model);
    Task<Result<string>> ChangePassword(ChangePasswordDto model);
}