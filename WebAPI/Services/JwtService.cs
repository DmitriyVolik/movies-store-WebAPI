using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using WebAPI.Options;

namespace WebAPI.Services;

public static class JwtService
{
    public static object GetJwtResponse(UserModel user, AuthConfig config)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var jwt = new JwtSecurityToken(
            issuer: config.Issuer,
            audience: config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key)), SecurityAlgorithms.HmacSha256));

        var response = new
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(jwt),
            user = user
        };
        
        return response;
    }
}