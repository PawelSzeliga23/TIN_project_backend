using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektSemestralnyTinWebApi.Context;
using ProjektSemestralnyTinWebApi.Models;
using ProjektSemestralnyTinWebApi.Security;
using LoginRequest = ProjektSemestralnyTinWebApi.Models.LoginRequest;
using RegisterRequest = ProjektSemestralnyTinWebApi.Models.RegisterRequest;

namespace ProjektSemestralnyTinWebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api")]
public class AuthenticationController : ControllerBase
{
    private readonly MasterContext _context;
    private readonly IConfiguration _configuration;

    public AuthenticationController(MasterContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelper.GetHashedPasswordAndSalt(model.Password);

        var us = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);

        if (us != null)
        {
            return BadRequest();
        }

        var user = new User()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelper.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1),
            RolesId = 1
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginRequest model)
    {
        var user = await _context.Users.Include(user => user.Roles).FirstOrDefaultAsync(u => u.Login == model.Login);
        if (user == null)
        {
            return Unauthorized();
        }

        var hashedPassword = SecurityHelper.GetHashedPasswordWithSalt(model.Password, user.Salt);
        if (user.Password != hashedPassword)
        {
            return Unauthorized();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Roles.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        Console.WriteLine(user.RefreshToken);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest model)
    {
        var user = await _context.Users.Include(user => user.Roles)
            .FirstOrDefaultAsync(u => u.RefreshToken.Trim() == model.RefreshToken.Trim());
        Console.WriteLine($"RefreshToken z żądania: {model.RefreshToken}");
        Console.WriteLine($"RefreshToken użytkownika: {user.RefreshToken}");

        if (user == null)
        {
            throw new SecurityTokenExpiredException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenExpiredException("Invalid refresh token");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Roles.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }
}