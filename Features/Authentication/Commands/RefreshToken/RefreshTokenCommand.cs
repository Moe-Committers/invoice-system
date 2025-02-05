using invoice_system.Utils.DTOs.Authentication;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;