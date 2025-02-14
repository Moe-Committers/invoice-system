using invoice_system.Features.Authentication.Commands.UpdateRolePermissions;
using invoice_system.Features.Authentication.Commands.UpdateUserRole;
using invoice_system.Features.Authentication.Queries.GetUser;
using invoice_system.Features.Authentication.Queries.GetUsers;
using invoice_system.Utils.Attributes;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.DTOs.UserMDto;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("roles/{roleId}/permissions")]
    // [RequirePermissions("manage_permissions")]
    public async Task<ActionResult<ApiResponse<RolePermissionsResponse>>> UpdateRolePermissions(long roleId, [FromBody] List<string> permissions)
    {
        var command = new UpdateRolePermissionsCommand
        {
            RoleId = roleId,
            Permissions = permissions
        };

        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpPut("users/{id}/role")]
    // [RequirePermissions("manage_users")]
    public async Task<ActionResult<ApiResponse<UserRoleResponse>>> UpdateUserRole(long id, [FromBody] UpdateUserRoleRequest request)
    {
        var command = new UpdateUserRoleCommand
        {
            UserId = id,
            RoleId = request.RoleId
        };

        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpGet("users")]
    [RequirePermissions("view_customers")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUsers([FromQuery] GetUsersQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

public class UpdateUserRoleRequest
{
    public long RoleId { get; set; }
}