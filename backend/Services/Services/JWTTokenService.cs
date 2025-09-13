using Expense.Tracker.Application.Interface.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expense.Tracker.Services.Services
{
    public class JWTTokenService(IConfiguration configuration) : ITokenService
    {
        public string GenerateAcessToken(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));
            SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );  

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
