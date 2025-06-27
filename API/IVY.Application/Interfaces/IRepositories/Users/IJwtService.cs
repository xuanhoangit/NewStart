using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Http;

namespace IVY.Application.Interfaces.Users;


public interface IJwtService 
{
    string GenerateJwtToken(EmployeeIdentity emp, IList<string> roles,TimeSpan expiry,IHttpContextAccessor httpContextAccessor);
}