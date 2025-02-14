using System.Security.Claims;
using invoice_system.Features.Authentication.Commands.Login;
using invoice_system.Features.Authentication.Commands.Logout;
using invoice_system.Features.Authentication.Commands.RefreshToken;
using invoice_system.Features.Authentication.Commands.Register;
using invoice_system.Features.Authentication.Commands.ToggleActive;
using invoice_system.Features.Authentication.Commands.UpdateProfile;
using invoice_system.Features.Authentication.Commands.UpdateUserPassword;
using invoice_system.Features.Authentication.Queries.GetUser;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Login(LoginCommand command)
    {
        var response = await _mediator.Send(command);

        SetRefreshTokenCookie(response.RefreshToken);

        return Ok(ResHelper.Success(response));
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Register(RegisterCommand command)
    {
        var response = await _mediator.Send(command);

        SetRefreshTokenCookie(response.RefreshToken);

        return Ok(ResHelper.Success(response));
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUser()
    {
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var query = new GetUserQuery(userId);
        var user = await _mediator.Send(query);
        return Ok(ResHelper.Success(ResHelper.Success(user)));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ApiResponse<ActionResult>> Logout()
    {
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return ResHelper.Errors<ActionResult>("Refresh token not found");
        }

        var command = new LogoutCommand(userId, refreshToken);
        var result = await _mediator.Send(command);

        if (result)
        {
            Response.Cookies.Delete("refreshToken");
            return ResHelper.Success<ActionResult>(null, null, "Success");
        }

        return ResHelper.Errors<ActionResult>("Errors!");
    }

    [HttpPut("update-profile/{userId}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> UpdatingProfile(long userId, [FromForm] UpdateProfileForm form)
    {
        var command = new UpdateProfileCommand
        {
            Id = userId,
            Name = form.Name,
            Img = form.Img
        };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpPut("update-password/{userId}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> UpdatingPassword(long userId, UpdateUserPassCommand request)
    {
        var command = request with { Id = userId };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpPut("toggle-status/{userId}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> ToggleStatus(long userId, ToggleActiveCommand request)
    {
        var command = request with { Id = userId };
        var result = await _mediator.Send(command);
        return Ok(ResHelper.Success(result));
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return NotFound(ResHelper.Errors<ActionResult>("Refresh token not found"));
        }

        var command = new RefreshTokenCommand(refreshToken);
        var response = await _mediator.Send(command);

        SetRefreshTokenCookie(response.RefreshToken);

        return Ok(ResHelper.Success(response));
    }

    private void SetRefreshTokenCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}