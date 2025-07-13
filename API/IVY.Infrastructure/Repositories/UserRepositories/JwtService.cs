using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IVY.Application.DTOs;
using IVY.Application.Interfaces.Users;
using IVY.Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IVY.Infrastructure.Repositories.UserRepositories;
public class JwtService : IJwtService
    {
    private readonly IConfiguration config;

    public JwtService(IConfiguration _config)
        {
        config = _config;
    }
        public string GenerateJwtToken(EmployeeIdentity emp, IList<string> roles,TimeSpan expiry)
        {
            try
            {
           
            var jwtSettings = config.GetSection("JWT");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
           
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, emp.Id.ToString()),//Sub sẽ đại diện cho Identifier 
                // new Claim(JwtRegisteredClaimNames.Email, emp.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(expiry),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
            
           

          
     
             }
            catch (System.Exception)
            {
                
                throw new Exception("An error occured while generating token");
            }
        }
        
}