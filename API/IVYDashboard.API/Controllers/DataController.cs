using IVY.Application.DTOs;
using IVY.Application.Interfaces.IRepository;
using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace IVYDashboard.Controllers;
public static class DataController
{
    public static async Task InitializeAccount(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<EmployeeIdentity>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        string[] roleNames = { RolesName.Admin, RolesName.Customer, RolesName.HumanSourceManager, RolesName.ProductManager, RolesName.Staff };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
        if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
        {
            var user = new EmployeeIdentity
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Department = "HR"
            };

            var result = await userManager.CreateAsync(user, "Txhoang11@123"); // Mật khẩu mặc định
           if(result.Succeeded) await userManager.AddToRoleAsync(user, RolesName.Admin);

        }
    }
}