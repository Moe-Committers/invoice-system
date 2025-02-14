using invoice_system.Utils.DTOs.UserMDto;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.UpdateRolePermissions;

public record UpdateRolePermissionsCommand : IRequest<RolePermissionsResponse>
{
    public long RoleId { get; init; }
    public List<string> Permissions { get; init; }
}