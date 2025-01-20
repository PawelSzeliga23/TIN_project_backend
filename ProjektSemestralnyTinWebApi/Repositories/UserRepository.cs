using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektSemestralnyTinWebApi.Context;
using ProjektSemestralnyTinWebApi.Models;

namespace ProjektSemestralnyTinWebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MasterContext _context;
    private readonly IConfiguration _configuration;

    public UserRepository(MasterContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<User> GetUserByAccessTokenAsync(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateActor = true,
            ClockSkew = TimeSpan.FromMinutes(2),
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            ValidateLifetime = false,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
                )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token!");
        }

        var userId = principal.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new SecurityTokenException($"Missing claim: {ClaimTypes.Name}!");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }
}