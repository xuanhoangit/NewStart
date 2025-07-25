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
using Microsoft.VisualBasic;
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
    private readonly TimeSpan accessTokenTimeExpire= TimeSpan.FromHours(1);
    private readonly int refreshTokenTimeExpireDays= 30;

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
        Avatar=account.Avatar,
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
        //  var refreshTokenFromCookie = "8dcddcd9-c54e-4e9f-a15e-38d9269b9ae9";
         var refreshTokenFromCookie = _httpContextAccessor.HttpContext?.Request?.Cookies["refreshToken"];
        var refAccountTOken =await _empManager.FindByEmailAsync("0965972715a@gmail.com");
        System.Console.WriteLine(refAccountTOken.RefreshToken);
        if (string.IsNullOrEmpty(refreshTokenFromCookie))
            return Result<dynamic>.Failure(ResultStatus.Unauthorized);

        var user = await _empManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenFromCookie);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            return Result<dynamic>.Failure(ResultStatus.Unauthorized);
        var roles=await _empManager.GetRolesAsync(user);
        
        var accessToken= _jwtService.GenerateJwtToken(user, roles, accessTokenTimeExpire);
        user.RefreshToken=Guid.NewGuid().ToString();
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(refreshTokenTimeExpireDays);
        var result=await _empManager.UpdateAsync(user);
        if (result.Succeeded)
        {   

             var response = _httpContextAccessor.HttpContext?.Response;
            response.Cookies.Append("accessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.Add(accessTokenTimeExpire),
                IsEssential = true,
                // Domain=""
            });
            response.Cookies.Append("refreshToken", user.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(10)),
                IsEssential = true,
                // Domain=""
            });
            
            
            System.Console.WriteLine("retoken"+user.RefreshToken);
            System.Console.WriteLine("lastupdate"+DateTime.UtcNow.ToString());
        }
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
        var cloudinary = new CloudinaryService();
            var newAccount = new EmployeeIdentity
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                DateOfBirth = model.DateOfBirth,
                FullName = model.FullName,
                Department = model.Department,
                Gender = model.Gender,
                CreateDate = DateTime.Today,
                Avatar=await cloudinary.UploadImageAsync(model.Avatar,"avatar")
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
            var accessToken= _jwtService.GenerateJwtToken(account, roles, accessTokenTimeExpire);
            account.RefreshToken=Guid.NewGuid().ToString();
            account.RefreshTokenExpiry = DateTime.UtcNow.AddDays(refreshTokenTimeExpireDays);
            var result=await _empManager.UpdateAsync(account);
            if (result.Succeeded)
            {   

                var response = _httpContextAccessor.HttpContext?.Response;
                response.Cookies.Append("accessToken", accessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.Add(accessTokenTimeExpire),
                    IsEssential = true,
                    // Domain=""
                });
                response.Cookies.Append("refreshToken", account.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(refreshTokenTimeExpireDays)),
                    IsEssential = true,
                    // Domain=""
                });
                
                
                System.Console.WriteLine("retoken"+account.RefreshToken);
                System.Console.WriteLine("lastupdate"+DateTime.UtcNow.ToString());
            }
    

        return Result<object>.Success(new {message="Đang nhập thành công"});
    }


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