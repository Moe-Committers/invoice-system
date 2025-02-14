using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using invoice_system.Database;
using invoice_system.Models;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace invoice_system.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly Db _db;
    private readonly IConfiguration _config;

    public RefreshTokenCommandHandler(Db db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var token = await _db.RefreshTokens
            .Include(u => u.user)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, ct);

        if (token == null)
        {
            throw new BadRequestExceptions("invalid token");
        }

        if (!token.isActive)
        {
            throw new BadRequestExceptions("token is not active");
        }

        var accessToken = await GenerateJwtToken(token.user);

        var newRefreshToken = new RefreshTokens
        {
            UserId = token.user.Id,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiredAt = DateTime.UtcNow.AddDays(10),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = "0.0.0.0"
        };

        _db.RefreshTokens.Remove(token);
        _db.RefreshTokens.Add(newRefreshToken);
        await _db.SaveChangesAsync(ct);

        var userDto = token.user.Adapt<UserDto>();
        userDto.Role = token.user.Role.Name ?? "Unknown";

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresIn = DateTime.UtcNow.AddDays(10),
            User = userDto
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