using invoice_system.Database;
using invoice_system.Utils.DTOs.UserMDto;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Commands.UpdateUserRole;

public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand, UserRoleResponse>
{
    private readonly Db _db;

    public UpdateUserRoleHandler(Db db)
    {
        _db = db;
    }

    public async Task<UserRoleResponse> Handle(UpdateUserRoleCommand request, CancellationToken ct)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, ct);

        if (user == null)
        {
            throw new NotFoundExceptions("User not found");
        }

        var role = await _db.Roles.FindAsync(new object[] { request.RoleId }, ct);
        if (role == null)
        {
            throw new NotFoundExceptions("Role not found");
        }

        user.RoleId = request.RoleId;
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);

        var updatedUser = await _db.Users
            .Include(u => u.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, ct);

        return new UserRoleResponse
        {
            Id = updatedUser.Id,
            Name = updatedUser.Name,
            Email = updatedUser.Email,
            Profile = updatedUser.Avatar,
            Role = updatedUser.Role.Name,
            Permissions = updatedUser.Role.RolePermissions
                .Select(rp => rp.Permission.Name)
                .ToList()
        };
    }
}