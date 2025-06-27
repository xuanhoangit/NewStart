using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Libs;
using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IVY.Application.Services;
public class ManageEmployersService : IManageEmployersService
{
        private readonly UserManager<EmployeeIdentity> _emp;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailSender _emailSender;
        public ManageEmployersService(UserManager<EmployeeIdentity> emp,
                                RoleManager<IdentityRole<Guid>> roleManager,
                                 IEmailSender emailSender)
        {
            _emp = emp;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
       
        public async Task<Result<bool>> LayOffPersonnel(UserRole model){
            var account= await _emp.FindByEmailAsync(model.Email);
            if(account==null)
                return Result<bool>.Failure(ResultStatus.NotFound);
            var isInRole= await _emp.IsInRoleAsync(account,model.Role);
            if(isInRole){
            var result=await _emp.RemoveFromRoleAsync(account,model.Role);
                if(result.Succeeded){
                // await _emailSender.SendEmailAsync(account.Email,"Terminate the contract",EmailTemplateHtml.RenderEmailNotificationBody(account.UserName,"","You are no longer an SLS salesperson."));
                return Result<bool>.Success(true);
                }
            }
            return Result<bool>.Failure(ResultStatus.NotFound);
                           
           
        }
        //Khóa tài khoản vĩnh viễm 
        public async Task<Result<bool>> BlockUser(string userId)
    {
        var account = await _emp.FindByIdAsync(userId);
        if (account == null)
            return Result<bool>.Failure(ResultStatus.NotFound);

        // Lock user vô thời hạn
        account.LockoutEnabled = true;
        account.LockoutEnd = DateTimeOffset.MaxValue;

        var result = await _emp.UpdateAsync(account);

        return Result<bool>.Success(result.Succeeded);
    }
    
    public async Task<Result<bool>> UnblockUser(string userId)
    {
        var user = await _emp.FindByIdAsync(userId);
        if (user == null)
            return Result<bool>.Failure(ResultStatus.NotFound);

        user.LockoutEnd = null;

        var result = await _emp.UpdateAsync(user);

        return Result<bool>.Success(result.Succeeded);
    }

    public async Task<Result<EmployeeIdentity>> GetEmployerInfomation(EmailUser model)
    {

        var account = await _emp.FindByEmailAsync(model.Email);
        if (account == null)
            return Result<EmployeeIdentity>.Failure(ResultStatus.NotFound);
        return Result<EmployeeIdentity>.Success(account); 
        }
      


        //Bổ nhiệm
        public async Task<Result<bool>> PositionAppointment(UserRole model)
        {
            
            var account=await _emp.FindByEmailAsync(model.Email);
        if (account != null)
        {
            var role = await _roleManager.RoleExistsAsync(model.Role);

            if (!role)
            {
                return Result<bool>.Failure(ResultStatus.BadRequest);
            }
            var isInRole = await _emp.IsInRoleAsync(account, model.Role);
            var rs = await _emp.AddToRoleAsync(account, model.Role);
            if (rs.Succeeded)
            {
                // await _emailSender.SendEmailAsync(account.Email, "Appointment employee", EmailTemplateHtml.RenderEmailNotificationBody(account.UserName, "", $"You are appointed as a {model.Role} of SLS."));
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure(ResultStatus.InternalError);
        }
        return Result<bool>.Failure(ResultStatus.NotFound);   
        }
}