using FoodieLionApi.Models;
using FoodieLionApi.Models.Entities;
using FoodieLionApi.Models.Enums;
using FoodieLionApi.Services.Interface;
using FoodieLionApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FoodieLionApi.Services;

public class LoginService : ILoginService
{
    private readonly FoodieLionDbContext _context;
    private readonly IConfiguration _configuration;

    public LoginService(FoodieLionDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> Login(string userNameOrEmail, string password)
    {
        var user =
            await _context.Users.FirstOrDefaultAsync(
                u => u.Name == userNameOrEmail || u.Email == userNameOrEmail
            ) ?? throw new FoodieLionException("User not found", ErrorCode.USER_NOT_FOUND);
        var bytes = Encoding.UTF8.GetBytes(password + user.Id);
        var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        password = Convert.ToBase64String(hash);
        if (user.Password != password)
        {
            throw new FoodieLionException("Password is incorrect", ErrorCode.INVALID_PASSWORD);
        }
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Role, user.Role.ToString()) }
            ),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
