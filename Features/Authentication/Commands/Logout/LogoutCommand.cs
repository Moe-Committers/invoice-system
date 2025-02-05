using MediatR;

namespace invoice_system.Features.Authentication.Commands.Logout;

public record LogoutCommand(long UserId, string RefreshToken) : IRequest<bool>;