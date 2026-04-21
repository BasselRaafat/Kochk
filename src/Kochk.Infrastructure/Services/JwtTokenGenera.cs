using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Kochk.Application.Common.Interfaces;
using Kochk.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kochk.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateTokenAsync(AppUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
        };

        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));

        var secretKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:SecurityKey"]!)
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"]!)),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha384)
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
