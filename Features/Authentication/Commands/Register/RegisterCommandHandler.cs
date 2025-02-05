using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using invoice_system.Database;
using invoice_system.Models;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Enums;
using invoice_system.Utils.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace invoice_system.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly Db _db;
    private readonly IConfiguration _config;

    public RegisterCommandHandler(Db db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken ct)
    {
        if (await _db.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new BadRequestExceptions("email already exist");
        }

        var DefaultRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "User", ct);

        var user = new Users
        {
            Name = request.Name,
            Age = request.Age,
            Email = request.Email,
            Status = Status.isActive,
            RoleId = DefaultRole.Id,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        return await GenerateAuthResponse(user);
    }

    private async Task<AuthResponse> GenerateAuthResponse(Users user)
    {
        var accessToken = await GenerateJwtToken(user);

        var refreshToken = new RefreshTokens
        {
            UserId = user.Id,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiredAt = DateTime.UtcNow.AddDays(10),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = "0.0.0.0"
        };

        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresIn = DateTime.UtcNow.AddDays(10),
            User = user.Adapt<UserDto>()
        };
    }

    private async Task<string> GenerateJwtToken(Users user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var userRole = await _db.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == user.RoleId);

        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, userRole.Name.ToString()),
            new Claim("status", user.Status.ToString())
        };

        if (userRole?.RolePermissions != null)
        {
            foreach (var rolePermission in userRole.RolePermissions)
            {
                claims.Add(new Claim("permission", rolePermission.Permission.Name));
            }
        }

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            signingCredentials: cred,
            expires: DateTime.UtcNow.AddDays(7)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}