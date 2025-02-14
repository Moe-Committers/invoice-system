using invoice_system.Database;
using invoice_system.Models;
using invoice_system.Utils.DTOs.UserMDto;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Commands.UpdateRolePermissions;

public class UpdateRolePermissionsHandler : IRequestHandler<UpdateRolePermissionsCommand, RolePermissionsResponse>
{
    private readonly Db _db;

    public UpdateRolePermissionsHandler(Db db)
    {
        _db = db;
    }

    public async Task<RolePermissionsResponse> Handle(UpdateRolePermissionsCommand request, CancellationToken ct)
    {

        var role = await _db.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, ct);

        if (role == null)
        {
            throw new NotFoundExceptions("Can't find the role you're looking for :(");
        }

        var allPermissions = await _db.Permissions.ToListAsync(ct);
        var validPermissionNames = allPermissions.Select(p => p.Name).ToList();

        var invalidPermissions = request.Permissions
            .Where(p => !validPermissionNames.Contains(p))
            .ToList();

        if (invalidPermissions.Any())
        {
            throw new BadRequestExceptions($"Invalid permissions: {string.Join(", ", invalidPermissions)}");
        }

        _db.RolePermissions.RemoveRange(role.RolePermissions);

        var permissionsToAdd = allPermissions
            .Where(p => request.Permissions.Contains(p.Name))
            .Select(p => new RolePermissions
            {
                RoleId = role.Id,
                PermissionId = p.Id,
                CreatedAt = DateTime.UtcNow
            });

        await _db.RolePermissions.AddRangeAsync(permissionsToAdd, ct);
        await _db.SaveChangesAsync(ct);

        var updatedRole = await _db.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, ct);

        return new RolePermissionsResponse
        {
            Id = updatedRole.Id,
            Name = updatedRole.Name,
            Permissions = updatedRole.RolePermissions
                .Select(rp => rp.Permission.Name)
                .ToList()
        };
    }
}