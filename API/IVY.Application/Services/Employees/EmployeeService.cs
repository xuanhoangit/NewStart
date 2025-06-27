using System.Net.Http;
using System.Security.Claims;
using System.Web;
using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Application.Interfaces.IServices.User;
using IVY.Application.Interfaces.Users;
using IVY.Domain.Libs;
using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IVY.Application.Services;

public class EmployeeService : IEmployeeService
{
    private static Dictionary<string, string> _refreshtoken = new();
    private readonly UserManager<EmployeeIdentity> _empManager;
    private readonly SignInManager<EmployeeIdentity> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly IMemoryCache _cache;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration config;
    private readonly IHttpClientFactory httpClientFactory;

    public EmployeeService(UserManager<EmployeeIdentity> empManager,
                                 SignInManager<EmployeeIdentity> signInManager,
                                 IEmailSender emailSender,
                                 IMemoryCache cache,
                                 IJwtService jwtService,
                                 IUnitOfWork uow,
                                 IHttpContextAccessor httpContextAccessor,
                                 IConfiguration config,
                                 IHttpClientFactory httpClientFactory)
        {   
            
            _empManager = empManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cache = cache;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        this.config = config;
        this.httpClientFactory = httpClientFactory;
    }
    
public async Task<CurrentUser>? GetCurrentUser()
{
    var httpContext = _httpContextAccessor.HttpContext;
    var user = httpContext?.User;

    if (user == null || !user.Identity?.IsAuthenticated == true)
    {
        return null;
    }

    var accountId = user.FindFirstValue(ClaimTypes.NameIdentifier); // hoặc "sub"
                                                                    // var email = user.FindFirstValue(ClaimTypes.Email);
    EmployeeIdentity? account = await _empManager.FindByIdAsync(accountId);
    if(account==null){
            return null;
    }
    var roles = user.Claims
        .Where(c => c.Type == ClaimTypes.Role || c.Type.EndsWith("role", StringComparison.OrdinalIgnoreCase))
        .Select(c => c.Value)
        .ToList();
    
    return new CurrentUser
    {
        Id = accountId,
        Email = account.Email,
        UserName=account.UserName,
        Gender =account.Gender==0?"Nam":"Nữ",
        DateOfBirth=account.DateOfBirth.ToString("dd/MM/yyyy"),
        Department=account.Department,
        FullName=account.FullName,
        CreateDate=account.CreateDate.ToString("dd/MM/yyyy"),
        Roles = roles
    };
}


   
    public async Task<Result<dynamic>> RefreshToken( )
    {   
         var refreshTokenFromCookie = _httpContextAccessor.HttpContext?.Request?.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshTokenFromCookie))
            return Result<dynamic>.Failure(ResultStatus.Unauthorized);

        var user = await _empManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenFromCookie);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            return Result<dynamic>.Failure(ResultStatus.Unauthorized);
        var roles=await _empManager.GetRolesAsync(user);
        user.RefreshToken = _jwtService.GenerateJwtToken(user, roles,TimeSpan.FromHours(1),_httpContextAccessor);
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
        await _empManager.UpdateAsync(user);
    
        return Result<dynamic>.Success("adad");
                   
        
    }



        
        public async Task<Result<EmployeeIdentity>> Register(RegisterDto model )
        {
           
            if (string.IsNullOrEmpty(model.Email))
            {
                return Result<EmployeeIdentity>.Failure(ResultStatus.BadRequest);
            }

            var account=await _empManager.FindByEmailAsync(model.Email);
            // if (model.Password != model.PasswordComfirm)
            // {
            //     return Result<EmployeeIdentity>.Failure(ResultStatus.BadRequest);
            // }
            if(account!=null)
            {   
                return Result<EmployeeIdentity>.Failure(ResultStatus.Conflict);
            }
            var newAccount = new EmployeeIdentity
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                DateOfBirth = model.DateOfBirth,
                FullName = model.FullName,
                Department = model.Department,
                Gender=model.Gender,
                CreateDate=DateTime.Today
            };

            var result = await _empManager.CreateAsync(newAccount);
            if (result.Succeeded)
            {   
                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                await _empManager.AddToRoleAsync(newAccount, model.Role);
                // var otpCode = HandleString.GenerateVerifyCode();
                // _cache.Set(model.Email, otpCode, TimeSpan.FromMinutes(5));

                // Gửi OTP qua email
              
    
              // cập nhật account với mật khẩu mới
            var token = await _empManager.GeneratePasswordResetTokenAsync(newAccount);

            var resetLink = $"{config["JWT:ValidAudience"]}/reset-password?email={model.Email}&token={HttpUtility.UrlEncode(token)}";
            System.Console.WriteLine(resetLink);
            // account.PasswordHash = _empManager.PasswordHasher.HashPassword(account, password);
            // await _empManager.UpdateAsync(account);

            await _emailSender.SendEmailAsync(model.Email, "Reset password",EmailTemplateHtml.GetResetPasswordEmail(resetLink,newAccount.UserName));

                
            return Result<EmployeeIdentity>.Created(newAccount);

            }
            return  Result<EmployeeIdentity>.Failure(ResultStatus.InternalError);
           
        }
 
        public async Task<Result<string>> ResendEmailConfirmation(ResendOTPDto model)
        {   
         
           
            var account = await _empManager.FindByEmailAsync(model.Email);
            if (account == null )
            {
                return Result<string>.Failure(ResultStatus.InternalError);
            }

            if (await _empManager.IsEmailConfirmedAsync(account))
            {
                return Result<string>.Failure(ResultStatus.Conflict);
            }

            if (!_cache.TryGetValue(model.Email, out string storedOtp))
            {   
                // Sinh OTP và lưu vào MemoryCache (hết hạn sau 5 phút)
                var otpCode = HandleString.GenerateVerifyCode();
                _cache.Set(model.Email, otpCode, TimeSpan.FromMinutes(5));

                // // Gửi OTP qua email
                // await _emailSender.SendEmailAsync(model.Email, "Confirm email",
                // EmailTemplateHtml.RenderEmailRegisterBody(model.Email, otpCode));
                return Result<string>.Success(otpCode);
            }
            return Result<string>.Failure(ResultStatus.NoContent);
          
        }
        public async Task<Result<bool>> ConfirmEmail(ConfirmEmailDto model)
        {   
           
           
            if (!_cache.TryGetValue(model.Email, out string storedOtp))
            {
                return Result<bool>.Failure(ResultStatus.BadRequest);
            }

            if (storedOtp != model.OtpCode)
            {
                return Result<bool>.Failure(ResultStatus.BadRequest);
            }

            var account = _empManager.FindByEmailAsync(model.Email).Result;
            if (account == null)
            {
                return Result<bool>.Failure(ResultStatus.NotFound);
            }

            if (account.EmailConfirmed)
            {
                return Result<bool>.Failure(ResultStatus.Conflict);
            }

            // Cập nhật trạng thái xác thực email
            account.EmailConfirmed = true;
            var a=await _empManager.UpdateAsync(account);

            // Xóa OTP khỏi cache sau khi xác nhận thành công
            _cache.Remove(model.Email);

            return Result<bool>.Success(true);
        }
        public async Task<Result<object>> Login(LoginDto model)
{
   

    // 1. Xác thực tài khoản
    var account = await _empManager.FindByEmailAsync(model.Email);
    if (account == null || !await _empManager.CheckPasswordAsync(account, model.Password))
        return Result<object>.Failure(ResultStatus.Unauthorized);

    var roles = await _empManager.GetRolesAsync(account);
    if (!roles.Any())
        return Result<object>.Failure(ResultStatus.Unauthorized);

    if (await _empManager.IsLockedOutAsync(account))
        return Result<object>.Failure(ResultStatus.Forbidden);

    if (!await _empManager.IsEmailConfirmedAsync(account))
        return Result<object>.Failure(ResultStatus.Forbidden);

    // 2. Tạo access token và refresh token
    // var accessTokenExpiry = model.RememberMe ? TimeSpan.FromDays(7) : TimeSpan.FromHours(1);
    account.RefreshToken = _jwtService.GenerateJwtToken(account, roles, TimeSpan.FromHours(1),_httpContextAccessor);
    account.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
    await _empManager.UpdateAsync(account);

   

    return Result<object>.Success(new {message="Đang nhập thành công"});
}

        // Đăng nhập
    // public async Task<Result<object>> Login(LoginDto model)
    // {
    //     var response = _httpContextAccessor.HttpContext?.Response;
    //     var account = await _empManager.FindByEmailAsync(model.Email);
    //     if (account == null || !await _empManager.CheckPasswordAsync(account, model.Password))
    //         return Result<object>.Failure(ResultStatus.Unauthorized);

    //     var roles=await _empManager.GetRolesAsync(account);
    //     if(!roles.Any())
    //         return Result<object>.Failure(ResultStatus.Unauthorized);
    //     if (await _empManager.IsLockedOutAsync(account))
    //     {
    //          return Result<object>.Failure(ResultStatus.Forbidden);
    //     }

    //     if (!await _empManager.IsEmailConfirmedAsync(account))
    //         return Result<object>.Failure(ResultStatus.Forbidden);

    //     var result = await _signInManager.PasswordSignInAsync(account, model.Password, true, false);
    //     if (result.Succeeded)
    //     {  
    //         System.Console.WriteLine("Success login"); 
    //         var accessTokenExpiry = model.RememberMe ? TimeSpan.FromDays(7) : TimeSpan.FromHours(1);
    //         var token = (TokenResponse)_jwtService.GenerateJwtToken(account,roles,accessTokenExpiry);

    //         account.RefreshToken = Guid.NewGuid().ToString();
    //         account.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
    //         await _empManager.UpdateAsync(account);

    //             // Lưu access token vào cookie
    //          response.Cookies.Append("accessToken", token.AccessToken, new CookieOptions
    //         {
    //             HttpOnly = true,
    //             Secure = true,
    //             SameSite = SameSiteMode.Strict,
    //             Expires = DateTimeOffset.UtcNow.AddMinutes(15)
    //         });

    //         // Lưu refresh token
    //         response.Cookies.Append("refreshToken", account.RefreshToken, new CookieOptions
    //         {
    //             HttpOnly = true,
    //             Secure = true,
    //             SameSite = SameSiteMode.Strict,
    //             Expires = DateTimeOffset.UtcNow.AddDays(7)
    //         });
    //         token.RefreshToken = account.RefreshToken;
    //         System.Console.WriteLine("hahaha");
    //         return Result<object>.Success(new {success=true,message="Đăng nhập thành công"});
    //     }
    //     return Result<object>.Failure(ResultStatus.BadRequest);       
    // }

    public async Task<Result<string>> ForgotPassword(ForgotPasswordDto model)
    {
        var account = await _empManager.FindByEmailAsync(model.Email);
        if (account == null)
        {
            // Không tiết lộ thông tin tài khoản có tồn tại hay không
            return Result<string>.Success(null);
        }
        // var password = HandleString.GenerateRandomString(16);
        // cập nhật account với mật khẩu mới
        var token = await _empManager.GeneratePasswordResetTokenAsync(account);

        var resetLink = $"{config["JWT:ValidAudience"]}/reset-password?email={model.Email}&token={HttpUtility.UrlEncode(token)}";
        System.Console.WriteLine(resetLink);
        // account.PasswordHash = _empManager.PasswordHasher.HashPassword(account, password);
        // await _empManager.UpdateAsync(account);

        await _emailSender.SendEmailAsync(model.Email, "Reset password", EmailTemplateHtml.GetResetPasswordEmail(resetLink, account.UserName));
        return Result<string>.Success("ahdhdha");
    }

        // Đặt lại mật khẩu
        public async Task<Result<string>> ResetPassword(ResetPasswordDto model)
        {
            var user = await _empManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Result<string>.Failure(ResultStatus.BadRequest);

            var result = await _empManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (!result.Succeeded)
                return Result<string>.Failure(ResultStatus.BadRequest);

            return Result<string>.Success("Đặt lại mật khẩu thành công");
        }

        public async Task<Result<string>> ChangePassword(ChangePasswordDto model)
        {
           
            var currentAccount=await GetCurrentUser();

            if (model.ConfirmNewPassword != model.NewPassword)
                return Result<string>.Failure(ResultStatus.BadRequest);

            var account = await _empManager.FindByEmailAsync(currentAccount.Email);
            if (account == null)
                
                return Result<string>.Failure(ResultStatus.BadRequest);

            var result = await _empManager.ChangePasswordAsync(account, model.Password, model.NewPassword);
    
            if (result.Succeeded){
                // var content="You just changed your password at"+ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                // await _emailSender.SendEmailAsync(currentAccount.Email, "Password changed",
                // EmailTemplateHtml.RenderEmailNotificationBody(currentAccount.Email,"Security Warning!", content));
                return Result<string>.Success("html email");
            }
            return Result<string>.Failure(ResultStatus.BadRequest);
        }
        public async Task<bool> VerifyRecaptcha(string token)
        {
            var secret = config["Recaptcha:SecretKey"];
            var client = httpClientFactory.CreateClient();
            
            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
                null);

            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);

            return result.success == true;
        }

}