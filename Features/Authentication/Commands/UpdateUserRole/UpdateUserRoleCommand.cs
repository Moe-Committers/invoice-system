using invoice_system.Utils.DTOs.UserMDto;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.UpdateUserRole;

public record UpdateUserRoleCommand : IRequest<UserRoleResponse>
{
    public long UserId { get; init; }
    public long RoleId { get; init; }
}
