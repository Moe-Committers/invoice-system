using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace invoice_system.Utils.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class RequirePermissionsAttribute : TypeFilterAttribute
{
    public RequirePermissionsAttribute(params string[] permissions)
        : base(typeof(RequirePermissionsFilter))
    {
        Arguments = new object[] { permissions };
    }
}

public class RequirePermissionsFilter : IAuthorizationFilter
{
    private readonly string[] _requiredPermissions;

    public RequirePermissionsFilter(string[] permissions)
    {
        _requiredPermissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userPermissions = context.HttpContext.User.Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value)
            .ToList();

        var hasPermission = _requiredPermissions.Any(permission =>
            userPermissions.Contains(permission));

        if (!hasPermission)
        {
            context.Result = new JsonResult(new
            {
                success = false,
                message = "You do not have permission to perform this action",
                data = (object)null,
                paginate = (object)null
            })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class RequireRolesAttribute : TypeFilterAttribute
{
    public RequireRolesAttribute(params string[] roles)
        : base(typeof(RequireRolesFilter))
    {
        Arguments = new object[] { roles };
    }
}

public class RequireRolesFilter : IAuthorizationFilter
{
    private readonly string[] _allowedRoles;

    public RequireRolesFilter(string[] roles)
    {
        _allowedRoles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userRole = context.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (!_allowedRoles.Contains(userRole))
        {
            context.Result = new JsonResult(new
            {
                success = false,
                message = "Unauthorized"
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}