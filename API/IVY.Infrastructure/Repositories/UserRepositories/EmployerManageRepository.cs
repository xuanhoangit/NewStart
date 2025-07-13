using IVY.Application.DTOs.Filters;
using IVY.Domain.Models.Users;
using IVY.Infrastructure.Data;
using IVY.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static IVY.Application.DTOs.User;

public class EmployerManageRepository:Repository<EmployeeIdentity>,IEmployerManageRepository
{   
    public EmployerManageRepository(IVYDbContext db):base(db)
    {
        
    }
    public async Task<List<UserWithRoleDTO>> GetEmployeeNoRole()
    {
        var query = from user in _context.Users.OfType<EmployeeIdentity>()
                    join ur in _context.UserRoles on user.Id equals ur.UserId into userRoles
                    from ur in userRoles.DefaultIfEmpty()
                    where ur == null

                    select new UserWithRoleDTO
                    {
                        Id = user.Id.ToString(),
                        Avatar = user.Avatar,
                        FullName = user.FullName,
                        Email = user.Email,
                        DateOfBirth = user.DateOfBirth.ToString("dd/MM/yyyy"),
                        CreateDate = user.CreateDate.ToString("dd/MM/yyyy"),
                        Department = user.Department.ToString(),
                        Gender = user.Gender == 0 ? "Nam" : "Nữ",
                        Roles = new List<string>()
                    };
            

        return await query.ToListAsync();

    }
    public async Task<List<UserWithRoleDTO>> GetFilteredEmployees(EmployeeFilter filter)
    {
        var query = from user in _context.Users.OfType<EmployeeIdentity>()
                    join ur in _context.UserRoles on user.Id equals ur.UserId
                    join role in _context.Roles on ur.RoleId equals role.Id
                    select new { user, role.Name };

        // Lọc theo filter
        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(x => x.user.Email.Contains(filter.Email));

        if (!string.IsNullOrWhiteSpace(filter.FullName))
            query = query.Where(x => x.user.FullName.Contains(filter.FullName));

        if (filter.Gender.HasValue)
            query = query.Where(x => x.user.Gender == filter.Gender);

        if (!string.IsNullOrWhiteSpace(filter.RoleName))
            query = query.Where(x => x.Name == filter.RoleName);

        // if (!string.IsNullOrWhiteSpace(filter.RoleRequest))
        //     query = query.Where(x => x.Name == filter.RoleRequest);

        // 👇 OrderBy bắt buộc trước Skip
        var grouped = await query
            .GroupBy(x => x.user)
            .OrderByDescending(g => g.Key.CreateDate) // 👈 chọn trường nào bạn muốn sắp xếp (ví dụ: FullName, Email, Id)
            .Skip((filter.Page - 1) * 10)
            .Take(10)
            .Select(g => new UserWithRoleDTO
            {
                Id = g.Key.Id.ToString(),
                Avatar= g.Key.Avatar,
                FullName = g.Key.FullName,
                Email = g.Key.Email,
                DateOfBirth=g.Key.DateOfBirth.ToString("dd/MM/yyyy"),
                CreateDate=g.Key.CreateDate.ToString("dd/MM/yyyy"),
                Department=g.Key.Department.ToString(),
                Gender = g.Key.Gender == 0 ? "Nam" : "Nữ", 
                Roles = g.Select(x => x.Name).Distinct().ToList()
            })
            .ToListAsync();

        return grouped;
    }

}