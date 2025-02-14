using invoice_system.Database;
using invoice_system.Utils.DTOs.Authentication;
using invoice_system.Utils.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Authentication.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly Db _db;

    public GetUserQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken ct)
    {
        var user = await _db.Users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Age = u.Age,
            Role = u.Role.Name,
            Status = u.Status,
            CreatedAt = u.CreatedAt,
            Avatar = u.Avatar
        }).FirstOrDefaultAsync(u => u.Id == request.UserId, ct);
        if (user == null)
        {
            throw new NotFoundExceptions("user not found");
        }
        return user;
    }
}