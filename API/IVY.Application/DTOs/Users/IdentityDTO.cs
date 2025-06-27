namespace IVY.Application.DTOs;

public static class RolesName
{
    public const string Admin = "Admin"; // ✅ Là hằng số
    public const string Staff = "Nhân viên bán hàng"; // ✅ Là hằng số
    public const string Customer = "Khách"; // ✅ Là hằng số
    public const string ProductManager = "Quản lý sản phẩm"; // ✅ Là hằng số
    public const string SaleManager = "Quản lý nhân sự"; // ✅ Là hằng số
}
public class RegisterDto
{
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string FullName { get; set; }
    public int Gender { get; set; }
    // public string? RefreshToken { get; set; }
    // public DateTime? RefreshTokenExpiry { get; set; }
    public string Department { get; set; }
    public DateTime CreateDate { get; set; }
}
public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; } = false;
        public string RecaptchaToken { get; set; }
}

public class ForgotPasswordDto
{
    public string Email { get; set; }
    public string RecaptchaToken { get; set; }
}
public class ResendOTPDto
{
    public string Email { get; set; }
}

public class ResetPasswordDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordDto
{
    public string? Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
public class CurrentUser{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string DateOfBirth { get; set; }
    public string FullName { get; set; }
    public string Department { get; set; }
    public string CreateDate { get; set; }
    public string Gender { get; set; }
    public List<string> Roles{ get; set; }
}
public class EmailUser{
    public string Email { get; set; }
}
public class UserRole{
    public string Email { get; set; }
    public string Role { get; set; }
}
public class TokenResponse{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
    public class ConfirmEmailDto
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
    }