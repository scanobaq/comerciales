using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using comerciales.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace comerciales.Infrastructure.Abstractions;

public class JwtTokenGenerator(IConfiguration config) : IJwtTokenGenerator
{
    public string GenerateToken(int userId, string userName, string userEmail, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, userEmail),
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
        issuer: config["Jwt:Issuer"],
        audience: config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(int.Parse(config["Jwt:ExpireMinutes"] ?? "60")),
        signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
