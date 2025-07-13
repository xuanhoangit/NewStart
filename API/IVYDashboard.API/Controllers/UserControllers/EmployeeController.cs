


using System.Security.Claims;
using IVY.Application.DTOs;
using IVY.Application.Interfaces.IServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IVYDashboard.API.Controllers.UserController
{
    [ApiController]
    [Route("api/account")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService emp;

        public EmployeeController(IEmployeeService emp)
        {
            this.emp = emp;
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // return Ok(new { Message = "Token hợp lệ", UserId = userId });
            return Ok(await emp.GetCurrentUser());
        }
        [HttpGet("user/{id}")]
        public IActionResult GetUser(string id)
        {
            return Ok();
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await emp.RefreshToken();
            return GetStatusReturn(result);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Xóa cookie access và refresh token
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Logged out" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterDto model)
        {   
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var result = await emp.Register(model);
            if (result.Status == ResultStatus.Created)
            {
                return CreatedAtAction(nameof(GetUser), new {id=result.Data.Id}, result.Data);
            }
            return GetStatusReturn(result);
        }
        // [HttpPost("resend-otp")]
        // public async Task<IActionResult> ResendEmailConfirmation(ResendOTPDto model)
        // {
        //     var result = await emp.ResendEmailConfirmation(model);
        //     return GetStatusReturn(result);
        // }
        // [HttpPatch("confirm")]
        // public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto model)
        // {
        //     var result = await emp.ConfirmEmail(model);
        //     return GetStatusReturn(result);
        // }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var isValidCaptcha = await emp.VerifyRecaptcha(model.RecaptchaToken);
            if (!isValidCaptcha)
            return BadRequest("Xác thực reCAPTCHA không hợp lệ.");
            var result = await emp.Login(model);
            return GetStatusReturn(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDto model)
        {   
            if (!await emp.VerifyRecaptcha(model.RecaptchaToken))
            return BadRequest("Xác thực reCAPTCHA không thành công");
            var result = await emp.ForgotPassword(model);
            return GetStatusReturn(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await emp.ResetPassword(model);
            return GetStatusReturn(result);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var result = await emp.ChangePassword(model);
            return GetStatusReturn(result);
        }
    } 
}
