using invoice_system.Utils.DTOs.Authentication;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;