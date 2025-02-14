using invoice_system.Utils.DTOs.Authentication;
using MediatR;

namespace invoice_system.Features.Authentication.Commands.Register;

public record RegisterCommand(string Name, int Age, string Email, string Password , int PhoneNumber) : IRequest<AuthResponse>;