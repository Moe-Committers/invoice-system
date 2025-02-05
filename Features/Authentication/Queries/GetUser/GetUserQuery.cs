using invoice_system.Utils.DTOs.Authentication;
using MediatR;

namespace invoice_system.Features.Authentication.Queries.GetUser;

public record GetUserQuery(long UserId) : IRequest<UserDto>;