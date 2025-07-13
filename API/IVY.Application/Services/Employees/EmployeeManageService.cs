using IVY.Application.DTOs;
using IVY.Application.DTOs.Filters;
using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Libs;
using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using static IVY.Application.DTOs.User;

namespace IVY.Application.Services;
public class EmployeeManagerService : IEmployeeManagerService
{
        private readonly UserManager<EmployeeIdentity> _emp;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork uow;

    public EmployeeManagerService(UserManager<EmployeeIdentity> emp,
                                RoleManager<IdentityRole<Guid>> roleManager,
                                 IEmailSender emailSender,
                                 IUnitOfWork uow)
        {
            _emp = emp;
            _roleManager = roleManager;
            _emailSender = emailSender;
        this.uow = uow;
    }
            public async Task<Result<UserWithRoleDTO>> UpdateProfile(RegisterDto model )
        {
           
            if (string.IsNullOrEmpty(model.Email))
            {
                return Result<UserWithRoleDTO>.Failure(ResultStatus.BadRequest);
            }

            var account=await _emp.FindByEmailAsync(model.Email);
            if(account==null)
            {   
                return Result<UserWithRoleDTO>.Failure(ResultStatus.NotFound);
            }
        var cloudinary = new CloudinaryService();

        account.UserName = model.Email;
        account.Email = model.Email;
        account.EmailConfirmed = true;
        account.DateOfBirth = model.DateOfBirth;
        account.FullName = model.FullName;
        account.Department = model.Department;
        account.Gender = model.Gender;
        if (model.Avatar != null) {
            account.Avatar = await cloudinary.UploadImageAsync(model.Avatar, "avatar");   
        }
            

            var result = await _emp.UpdateAsync(account);
            if (result.Succeeded)
            {   
               
                if(model.Role!=null)
                await _emp.AddToRoleAsync(account, model.Role);

            return Result<UserWithRoleDTO>.Success(new UserWithRoleDTO
                {
                    Id = account.Id.ToString(),
                    Avatar = account.Avatar,
                    FullName = account.FullName,
                    Email = account.Email,
                    DateOfBirth = account.DateOfBirth.ToString("dd/MM/yyyy"),
                    CreateDate = account.CreateDate.ToString("dd/MM/yyyy"),
                    Department = account.Department.ToString(),
                    Gender = account.Gender == 0 ? "Nam" : "Nữ",
                    Roles = await _emp.GetRolesAsync(account)
                });

            }
            return  Result<UserWithRoleDTO>.Failure(ResultStatus.InternalError);
           
        }
 
    public async Task<Result<List<UserWithRoleDTO>>> GetEmployeeNoRole()
    {
        var employees = await uow.Employee.GetEmployeeNoRole();
        return Result<List<UserWithRoleDTO>>.Success(employees);
    }
        public async Task<Result<List<UserWithRoleDTO>>> GetEmployees(EmployeeFilter filter)
    {
        var employees = await uow.Employee.GetFilteredEmployees(filter);
        return Result<List<UserWithRoleDTO>>.Success(employees);
    }
        
        public async Task<Result<bool>> RevokeAccess(UserRole model)
        {
            var account = await _emp.FindByEmailAsync(model.Email);
            if (account == null)
                return Result<bool>.Failure(ResultStatus.NotFound, "User not found.");

            var isInRole = await _emp.IsInRoleAsync(account, model.Role);
            if (!isInRole)
                return Result<bool>.Failure(ResultStatus.BadRequest, $"User is not in role '{model.Role}'.");

            var result = await _emp.RemoveFromRoleAsync(account, model.Role);
            if (result.Succeeded)
            {
                // Gửi email nếu cần
                // await _emailSender.SendEmailAsync(account.Email, "Role removed", ...);
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ResultStatus.InternalError, "Failed to remove role.");
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